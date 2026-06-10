using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnificationOfRussianLands
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        //состояния игры (меню, игра, проигрыш, победа)
        private enum GameState
        {
            MainMenu,
            CardSwiping,
            GameOver,
            Victory
        }
        private GameState _currentState = GameState.MainMenu;
        //кнопки выхода
        private Rectangle _newGameButton;
        private Rectangle _exitButton;
        private Rectangle _backToMenuButton;
        private Rectangle _winToMenuButton;
        private MouseState _previousMouseState;
        //начальное смещение при перетаскивании
        private float _dragStartOffset = 0f;
        //музыка
        private Song _menuMusic;
        private Song _gameMusic;
        private Song _winMusic;
        private Song _loseMusic;

        //звуки
        private SoundEffect _newGameSound;
        private SoundEffect _cardSwipeSound;
        //шрифт
        private SpriteFont _gameFont;

        //новая система карт
        private List<Card> _allCards;           //все карточки из CardBase
        private List<Card> _currentDeck;        //текущая колода (копия, из которой удаляем)
        private Card _currentCard;               //текущая карточка (объект)
        private int _nextCardId = 0;             //ID следующей карточки (0 = случайная)
        private int _delayedCardId = 0;          //ID отложенной карточки
        private int _delayCounter = 0;           //счетчик отложенной карточки
        private string _gameOverReason = "";     //причина смерти
        private string _playerName = "Князь";   //имя и прозвище князя
        private string _playerNickname = "";

        //счетчик лет
        private int _yearsPassed = 0;            //сколько лет прошло с начала правления

        //фоны
        private Texture2D _startMenu;
        private Texture2D _gameBackground;
        private Texture2D _loseScreen;
        private Texture2D _winScreen;
        private Texture2D _cardTexture;

        //наклон портрета
        //ткущ угол+нужный угол+скорость доводки (меньше=плавнее)
        private float _currentRotation = 0f;
        private float _targetRotation = 0f;
        private float _rotationSpeed = 0.1f;

        //смещение портрета при свайпе
        private float _currentDragOffset = 0f;
        private float _targetDragOffset = 0f;
        private float _dragSpeed = 0.3f;

        //перетаскивание карты мышью
        private bool _isDragging = false;

        //хитбоксы половин карты
        private Rectangle _leftHalf;
        private Rectangle _rightHalf;
        private Rectangle _fullCardHitbox;

        //иконки статов
        private Texture2D _religionIcon;
        private Texture2D _peopleIcon;
        private Texture2D _armyIcon;
        private Texture2D _moneyIcon;

        //круги влияния зеленый и красный и белый
        private Texture2D _circleTexture;
        private Texture2D _greenCircleTexture;
        private Texture2D _redCircleTexture;
        //белая заливка
        private Texture2D _whitePixel;

        //панель черная под текст
        private Texture2D _darkOverlay;

        //портреты
        private Texture2D _currentPortrait;

        //имя сына (выбирается в карточке Н1)
        private string _sonName = "";

        //статы
        private float _religionValue = 50;
        private float _peopleValue = 50;
        private float _armyValue = 50;
        private float _moneyValue = 50;

        //имя вопрос и выборы
        private string _characterName = "";
        private string _currentEventText = "";
        private string _tempLeftText = "";
        private string _tempRightText = "";

        //размеры личика
        private int _portraitWidth = 320;
        private int _portraitHeight = 320;

        //масштаб вопроса
        private float _eventTextScale = 0.85f;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            //размеры окна игры
            _graphics.PreferredBackBufferWidth = 1376;
            _graphics.PreferredBackBufferHeight = 768;

            //применяем настройки экрана
            _graphics.ApplyChanges();

            //хитбоксы под кнопки
            _backToMenuButton = new Rectangle(395, 194, 600, 96); //поражение в меню
            _winToMenuButton = new Rectangle(479, 212, 427, 88);   //победа в меню
            _newGameButton = new Rectangle(549, 529, 290, 75);    //новая игра
            _exitButton = new Rectangle(615, 626, 157, 47);       //выход

            base.Initialize();
        }

        protected override void LoadContent()
        {
            //шрифт для текста
            _gameFont = Content.Load<SpriteFont>("Fonts/main_font");

            //создаем инструмент для рисования картинок
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            //громкость звуковых эффектов
            SoundEffect.MasterVolume = 0.5f;

            //фоны
            _startMenu = Content.Load<Texture2D>("Backgrounds/start");
            _gameBackground = Content.Load<Texture2D>("Backgrounds/default");
            _cardTexture = Content.Load<Texture2D>("UI/card_background");

            //музыка
            _menuMusic = Content.Load<Song>("Music/menu");
            _gameMusic = Content.Load<Song>("Music/river_song");
            _winMusic = Content.Load<Song>("Music/win");
            _loseMusic = Content.Load<Song>("Music/lose");
            MediaPlayer.Play(_menuMusic);
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = 0.15f;  //громкость музыки в главном меню 15%

            //звуки
            _newGameSound = Content.Load<SoundEffect>("Sounds/new_game");
            _cardSwipeSound = Content.Load<SoundEffect>("Sounds/card");

            //картинки иконок статов
            _religionIcon = Content.Load<Texture2D>("Icons/relig_icon2");
            _peopleIcon = Content.Load<Texture2D>("Icons/man_icon2");
            _armyIcon = Content.Load<Texture2D>("Icons/war_icon2");
            _moneyIcon = Content.Load<Texture2D>("Icons/money_icon2");

            //стартовый портрет персонажа
            _currentPortrait = Content.Load<Texture2D>("Portraits/mother");

            //экраны проигрыша и победы
            _loseScreen = Content.Load<Texture2D>("Backgrounds/lose");
            _winScreen = Content.Load<Texture2D>("Backgrounds/win");

            //кружки-индикаторы (белый, зеленый, красный)
            _circleTexture = Content.Load<Texture2D>("UI/circle");
            _greenCircleTexture = Content.Load<Texture2D>("UI/green_circle");
            _redCircleTexture = Content.Load<Texture2D>("UI/red_circle");

            //создаем белый пиксель для закрашивания полосок статов
            _whitePixel = new Texture2D(GraphicsDevice, 1, 1);
            _whitePixel.SetData(new[] { Color.White });

            //создаем полупрозрачную черную плашку под текст выбора
            _darkOverlay = new Texture2D(GraphicsDevice, 1, 1);
            _darkOverlay.SetData(new[] { new Color(0, 0, 0, 100) });
        }

        //методы для работы с карточками

        //создание колоды при перезапуске
        private void InitDeck()
        {
            //получаем все карточки из базы
            _allCards = CardBase.CreateDeck();
            //создаем копию для текущей колоды (из которой будем удалять)
            _currentDeck = new List<Card>(_allCards);
            //начинаем с карточки (Id = 100-тутор)
            _currentCard = _currentDeck.FirstOrDefault(c => c.Id == 100);
            _nextCardId = 0;
            _delayedCardId = 0;
            _delayCounter = 0;
            _gameOverReason = "";
            //сброс счетчика лет при новой игре
            _yearsPassed = 0;
            //сброс имени сына
            _sonName = "";
            //загружаем первую карточку
            LoadCardContent();
        }

        //удаляем карточки из колоды по списку id (ии написал)
        private void RemoveCardsByIds(List<int> idsToRemove)
        {
            if (idsToRemove == null) return;
            foreach (int id in idsToRemove)
            {
                var card = _currentDeck.FirstOrDefault(c => c.Id == id);
                if (card != null)
                    _currentDeck.Remove(card);
            }
        }

        //загружаем карточку (имя выборы вопрос)
        private void LoadCardContent()
        {
            if (_currentCard == null) return;

            _characterName = _currentCard.CharacterName;
            _currentEventText = _currentCard.Description;
            _tempLeftText = _currentCard.LeftChoiceText;
            _tempRightText = _currentCard.RightChoiceText;

            try
            {
                _currentPortrait = Content.Load<Texture2D>("Portraits/" + _currentCard.PortraitName);
            }
            catch
            {
                //если портрет не загрузился, ниче не меняем
            }
        }

        //загрузка карточки по id
        private void LoadCardById(int id)
        {
            _currentCard = _currentDeck.FirstOrDefault(c => c.Id == id);
            if (_currentCard != null)
            {
                LoadCardContent();
            }
        }

        //Получение случайной карточки из пула (все карточки с Id >= 301)
        private Card GetRandomCard()
        {
            var randomCards = _currentDeck.Where(c => c.Id >= 301).ToList();
            if (randomCards.Count == 0) return null;
            Random rand = new Random();
            return randomCards[rand.Next(randomCards.Count)];
        }


        //Проверка смерти от статов (0 или 100)
        private bool CheckDeathByStats()
        {
            if (_religionValue <= 0) { _gameOverReason = "Вера упала до нуля. Народ отвернулся от Бога, а князя предали анафеме."; return true; }
            if (_religionValue >= 100) { _gameOverReason = "Вера возросла до предела. Вы ушли в монастырь, оставив мирской престол."; return true; }

            if (_peopleValue <= 0) { _gameOverReason = "Людской ресурс иссяк. Земли опустели, а выжившие смерды сожгли терем."; return true; }
            if (_peopleValue >= 100) { _gameOverReason = "Народ заполонил всё. Крестьянское вече свергло вас ради свободы."; return true; }

            if (_armyValue <= 0) { _gameOverReason = "Дружина пала до нуля. Некому защищать границы — город сожжен набегом."; return true; }
            if (_armyValue >= 100) { _gameOverReason = "Войско стало слишком сильным. Воевода поднял мятеж и забрал корону."; return true; }

            if (_moneyValue <= 0) { _gameOverReason = "Казна полностью разорилась. Бояре продали княжество за долги."; return true; }
            if (_moneyValue >= 100) { _gameOverReason = "Золото вышло за все пределы. Вас задушили в мешке с монетами ради наживы."; return true; }
            return false;
        }


        //метод для рисования текста с переносом по ширине (написал ИИ)
        private void DrawTextInRect(string text, Rectangle rect, Color color, float scale = 1f)
        {
            if (string.IsNullOrEmpty(text) || _gameFont == null) return;

            string[] words = text.Split(' ');
            List<string> lines = new List<string>();
            StringBuilder currentLine = new StringBuilder();

            foreach (string word in words)
            {
                string testLine = currentLine.Length == 0 ? word : currentLine.ToString() + " " + word;
                Vector2 size = _gameFont.MeasureString(testLine) * scale;

                if (size.X > rect.Width)
                {
                    if (currentLine.Length > 0)
                    {
                        lines.Add(currentLine.ToString());
                        currentLine.Clear();
                        currentLine.Append(word);
                    }
                    else
                    {
                        lines.Add(word);
                    }
                }
                else
                {
                    if (currentLine.Length > 0)
                        currentLine.Append(" ");
                    currentLine.Append(word);
                }
            }
            if (currentLine.Length > 0)
                lines.Add(currentLine.ToString());

            float lineHeight = _gameFont.MeasureString("A").Y * scale;
            float totalHeight = lines.Count * lineHeight;
            float startY = rect.Y + (rect.Height - totalHeight) / 2;

            for (int i = 0; i < lines.Count; i++)
            {
                Vector2 size = _gameFont.MeasureString(lines[i]) * scale;
                float x = rect.X + (rect.Width - size.X) / 2;
                float y = startY + i * lineHeight;
                _spriteBatch.DrawString(_gameFont, lines[i], new Vector2(x, y), color, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
            }
        }

        //метод начала новой игры
        private void StartNewGame()
        {
            //сброс статов
            _religionValue = 50;
            _peopleValue = 50;
            _armyValue = 50;
            _moneyValue = 50;

            _currentState = GameState.CardSwiping;

            //создаем колоду по новой системе
            InitDeck();

            ResetPortraitState();
        }

        private void ApplyChoice(bool isLeftChoice)
        {
            if (_currentCard == null) return;

            //звук перелистывания (громкость 20% через SoundEffectInstance)
            SoundEffectInstance swipeInstance = _cardSwipeSound.CreateInstance();
            swipeInstance.Volume = 0.2f;
            swipeInstance.Play();

            //изменения статов лево право
            if (isLeftChoice)
            {
                _religionValue += _currentCard.LeftReligionChange;
                _peopleValue += _currentCard.LeftPeopleChange;
                _armyValue += _currentCard.LeftArmyChange;
                _moneyValue += _currentCard.LeftMoneyChange;
            }
            else
            {
                _religionValue += _currentCard.RightReligionChange;
                _peopleValue += _currentCard.RightPeopleChange;
                _armyValue += _currentCard.RightArmyChange;
                _moneyValue += _currentCard.RightMoneyChange;
            }

            //ограничение статов после изменений
            _religionValue = Math.Clamp(_religionValue, 0, 100);
            _peopleValue = Math.Clamp(_peopleValue, 0, 100);
            _armyValue = Math.Clamp(_armyValue, 0, 100);
            _moneyValue = Math.Clamp(_moneyValue, 0, 100);

            //смена прозвища
            if (isLeftChoice && !string.IsNullOrEmpty(_currentCard.NicknameLeft))
            {
                _playerNickname = _currentCard.NicknameLeft;
            }
            else if (!isLeftChoice && !string.IsNullOrEmpty(_currentCard.NicknameRight))
            {
                _playerNickname = _currentCard.NicknameRight;
            }

            //сохранение имени сына (карточка Н1, Id = 101)
            if (_currentCard.Id == 101)
            {
                if (isLeftChoice)
                    _sonName = "Ярополк";
                else
                    _sonName = "Владимир";
            }

            //увеличение счетчика лет (только если карточка добавляет год)
            if (_currentCard.AddsYear)
            {
                _yearsPassed++;
            }

            //проверка на мгновенную смерть от условий карточки (как с кузнецом)
            if (isLeftChoice && _currentCard.InstantDeathLeft)
            {
                _gameOverReason = _currentCard.DeathReason;
                _currentState = GameState.GameOver;
                MediaPlayer.Stop();
                MediaPlayer.Volume = 0.05f;  //громкость музыки поражения 5%
                MediaPlayer.Play(_loseMusic);
                MediaPlayer.IsRepeating = true;
                return;
            }
            if (!isLeftChoice && _currentCard.InstantDeathRight)
            {
                _gameOverReason = _currentCard.DeathReason;
                _currentState = GameState.GameOver;
                MediaPlayer.Stop();
                MediaPlayer.Volume = 0.05f;  //громкость музыки поражения 5%
                MediaPlayer.Play(_loseMusic);
                MediaPlayer.IsRepeating = true;
                return;
            }

            //проверка на статы 
            if (CheckDeathByStats())
            {
                _currentState = GameState.GameOver;
                MediaPlayer.Stop();
                MediaPlayer.Volume = 0.05f;  //громкость музыки поражения 5%
                MediaPlayer.Play(_loseMusic);
                MediaPlayer.IsRepeating = true;
                return;
            }

            //вычеркиваем карточки из колоды
            if (isLeftChoice)
                RemoveCardsByIds(_currentCard.RemoveCardsLeft);
            else
                RemoveCardsByIds(_currentCard.RemoveCardsRight);

            //настройка отложенных карт
            if (isLeftChoice)
            {
                if (_currentCard.DelayedCardIdLeft != 0 && _currentCard.DelayCounterLeft > 0)
                {
                    _delayedCardId = _currentCard.DelayedCardIdLeft;
                    _delayCounter = _currentCard.DelayCounterLeft;
                }
            }
            else
            {
                if (_currentCard.DelayedCardIdRight != 0 && _currentCard.DelayCounterRight > 0)
                {
                    _delayedCardId = _currentCard.DelayedCardIdRight;
                    _delayCounter = _currentCard.DelayCounterRight;
                }
            }

            //настрйока некст карты
            if (isLeftChoice)
                _nextCardId = _currentCard.NextCardIdLeft;
            else
                _nextCardId = _currentCard.NextCardIdRight;

            //обработка рандома для БЖ3 и БЖ4
            if (_currentCard.Id == 104)
            {
                Random rand = new Random();
                if (rand.Next(10) < 3) //30% смерть
                    _nextCardId = 107; //смерть
                else
                    _nextCardId = 106; //ураа
            }

            if (_currentCard.Id == 105 && !isLeftChoice)
            {
                Random rand = new Random();
                if (rand.Next(2) == 0) //50% смерть
                    _nextCardId = 107; //смерть
                else
                    _nextCardId = 106; //ураа
            }
            LoadNextCard();
        }


        //загрузка некст карточки (с учетом отложенных и переходов)
        private void LoadNextCard()
        {
            //проверка отложенной карточки
            if (_delayedCardId != 0 && _delayCounter > 0)
            {
                _delayCounter--;
                if (_delayCounter == 0)
                {
                    LoadCardById(_delayedCardId);
                    _delayedCardId = 0;
                    if (_currentCard != null)
                    {
                        //удаляем отложенную карточку после использования
                        RemoveCardById(_currentCard.Id);
                        return;
                    }
                }
            }

            //проверка следующей карточки
            if (_nextCardId != 0)
            {
                LoadCardById(_nextCardId);
                _nextCardId = 0;
                if (_currentCard != null)
                {
                    //удаляем после использования
                    RemoveCardById(_currentCard.Id);
                    return;
                }
            }

            //если нет указанной след карточки, то юзаем случайную
            _currentCard = GetRandomCard();
            if (_currentCard != null)
            {
                LoadCardContent();
                //также удаляем после юза
                RemoveCardById(_currentCard.Id);
            }
            else
            {
                //если рандом карты кончились
                if (!string.IsNullOrEmpty(_sonName))
                {
                    //есть сын - победа
                    _currentState = GameState.Victory;
                    MediaPlayer.Stop();
                    MediaPlayer.Volume = 0.05f;
                    MediaPlayer.Play(_winMusic);
                    MediaPlayer.IsRepeating = true;
                }
                else
                {
                    //нет сына - проигрыш
                    _gameOverReason = "После твоей кончины, некому было передать престол...";
                    _currentState = GameState.GameOver;
                    MediaPlayer.Stop();
                    MediaPlayer.Volume = 0.05f;
                    MediaPlayer.Play(_loseMusic);
                    MediaPlayer.IsRepeating = true;
                }
            }
        }

        //удаление карточки из текущей колоды по id
        private void RemoveCardById(int id)
        {
            var card = _currentDeck.FirstOrDefault(c => c.Id == id);
            if (card != null)
                _currentDeck.Remove(card);
        }

        //обновленная механика смены карточек
        protected override void Update(GameTime gameTime)
        {
            //Узнаем, где сейчас находится курсор мышки и нажаты ли кнопки
            MouseState mouseState = Mouse.GetState();
            Point mousePos = new Point(mouseState.X, mouseState.Y);

            //Задаем базовые размеры карточки события
            int cardWidth = 411;
            int cardHeight = 702;

            //Рассчитываем координаты для выравнивания карточки строго по центру экрана (1376x768)
            int cardX = (1376 - cardWidth) / 2;
            int cardY = (768 - cardHeight) / 2;

            //Находим точные экранные координаты для размещения портрета персонажа внутри карты
            int portraitX = cardX + (cardWidth - _portraitWidth) / 2;
            int portraitY = cardY + 270;

            //Инициализируем (обновляем каждый кадр) хитбоксы для мыши
            _fullCardHitbox = new Rectangle(portraitX, portraitY, _portraitWidth, _portraitHeight); //Вся область портрета
            _leftHalf = new Rectangle(portraitX, portraitY, _portraitWidth / 2, _portraitHeight);    //Левая половинка для свайпа влево
            _rightHalf = new Rectangle(portraitX + _portraitWidth / 2, portraitY, _portraitWidth / 2, _portraitHeight); //Правая половинка для свайпа вправо

            //проверка где мы щас
            switch (_currentState)
            {
                //еслив меню
                case GameState.MainMenu:
                    //ищем клик
                    if (mouseState.LeftButton == ButtonState.Pressed && _previousMouseState.LeftButton == ButtonState.Released)
                    {
                        //если клик на новую игру
                        if (_newGameButton.Contains(mousePos))
                        {

                            _playerNickname = "Младой";
                            _playerName = "Владимир";
                            //звук начала игры (громкость 10%)
                            SoundEffectInstance newGameInstance = _newGameSound.CreateInstance();
                            newGameInstance.Volume = 0.1f;
                            newGameInstance.Play();
                            MediaPlayer.Stop();
                            MediaPlayer.Volume = 0.05f;  //громкость музыки в игре 5%
                            MediaPlayer.Play(_gameMusic);
                            MediaPlayer.IsRepeating = true;
                            StartNewGame();
                        }
                        //если клик на выход
                        if (_exitButton.Contains(mousePos))
                        {
                            Exit();
                        }
                    }
                    break;

                //если играем
                case GameState.CardSwiping:
                    //Вычисляем центр портрета по оси X
                    float portraitCenterX = portraitX + _portraitWidth / 2f;

                    //Параметры для смещения и поворота
                    float deadZoneHalfWidth = _portraitWidth / 6f;  //половинка мертвой зоны (1/6 от ширины портрета)
                    float maxOffset = 40f;                          //максимальное смещение в пикселях
                    float maxOvershoot = 150f;                     //расстояние для максимального поворота
                    float maxRotationAngle = 0.35f;                //максимальный угол поворота
                    float followSpeed = 0.15f;                      //скорость следования за мышью

                    //1. Фиксируем момент нажатия
                    if (mouseState.LeftButton == ButtonState.Pressed && _previousMouseState.LeftButton == ButtonState.Released)
                    {
                        //Если кликнули по карте — начинаем перетаскивание
                        if (_fullCardHitbox.Contains(mousePos))
                        {
                            _isDragging = true;
                            //Запоминаем точку начала перетаскивания относительно центра портрета
                            _dragStartOffset = 0f;
                        }
                    }

                    //2. Если мышь удерживается нажатой
                    if (_isDragging && mouseState.LeftButton == ButtonState.Pressed)
                    {
                        //Считаем, насколько далеко мышь ушла от центра карты по горизонтали
                        float deltaX = mousePos.X - portraitCenterX;

                        //Целевое смещение (ограниченное)
                        float targetOffset = Math.Clamp(deltaX, -maxOffset, maxOffset);

                        //Плавно двигаемся к цели (чем меньше число, тем медленнее)
                        _currentDragOffset = MathHelper.Lerp(_currentDragOffset, targetOffset, followSpeed);

                        //Считаем поворот
                        if (Math.Abs(deltaX) <= deadZoneHalfWidth)
                        {
                            //Мышь в мертвой зоне — поворота нет
                            _targetRotation = 0f;
                        }
                        else
                        {
                            //Мышь вышла за пределы мертвой зоны
                            //Считаем, насколько далеко вышли за пределы мертвой зоны
                            float overshoot = Math.Abs(deltaX) - deadZoneHalfWidth;
                            float rotationFactor = Math.Clamp(overshoot / maxOvershoot, 0f, 1f);
                            _targetRotation = Math.Sign(deltaX) * rotationFactor * maxRotationAngle;
                        }

                        //Плавно доводим угол (чтобы не дергался)
                        _currentRotation = MathHelper.Lerp(_currentRotation, _targetRotation, _rotationSpeed);
                    }
                    else
                    {
                        //Если мышь не зажата, возвращаем все в ноль
                        _currentDragOffset = MathHelper.Lerp(_currentDragOffset, 0f, 0.05f); //плавный возврат смещения
                        _targetRotation = 0f;
                        _currentRotation = MathHelper.Lerp(_currentRotation, _targetRotation, _rotationSpeed);
                    }

                    //3. Момент отпускания мыши (принятие решения)
                    if (_isDragging && mouseState.LeftButton == ButtonState.Released)
                    {
                        _isDragging = false; //Больше не тащим

                        //Задаем порог, при котором свайп считается успешным (по углу)
                        float swipeThreshold = 0.2f;

                        if (_currentRotation < -swipeThreshold)
                        {
                            //Карта сильно отклонена влево — применяем левый выбор
                            ApplyChoice(true);
                            ResetPortraitState();
                        }
                        else if (_currentRotation > swipeThreshold)
                        {
                            //Карта сильно отклонена вправо — применяем правый выбор
                            ApplyChoice(false);
                            ResetPortraitState();
                        }
                        //Если отпустили в центральной зоне (или близко к ней) — ничего не происходит, карта просто вернется в центр
                    }
                    break;

                //если проиграли
                case GameState.GameOver:
                    //ищем клик
                    if (mouseState.LeftButton == ButtonState.Pressed && _previousMouseState.LeftButton == ButtonState.Released)
                    {
                        if (_backToMenuButton.Contains(mousePos))
                        {
                            //звук начала игры (громкость 10%)
                            SoundEffectInstance newGameInstance = _newGameSound.CreateInstance();
                            newGameInstance.Volume = 0.1f;
                            newGameInstance.Play();
                            MediaPlayer.Stop();
                            MediaPlayer.Volume = 0.15f;  //громкость музыки в главном меню 15%
                            MediaPlayer.Play(_menuMusic);
                            MediaPlayer.IsRepeating = true;
                            _currentState = GameState.MainMenu;
                            ResetPortraitState();
                        }
                    }
                    break;

                //если выиграли
                case GameState.Victory:
                    //клик
                    if (mouseState.LeftButton == ButtonState.Pressed && _previousMouseState.LeftButton == ButtonState.Released)
                    {
                        if (_winToMenuButton.Contains(mousePos))
                        {
                            MediaPlayer.Stop();
                            MediaPlayer.Volume = 0.15f;  //громкость музыки в главном меню 15%
                            MediaPlayer.Play(_menuMusic);
                            MediaPlayer.IsRepeating = true;
                            _currentState = GameState.MainMenu;
                            ResetPortraitState();
                        }
                    }
                    break;
            }

            //запоминаем состояние лкм, чтобы понять когда отпустим кнпоку
            _previousMouseState = mouseState;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            //очищаем экран и заливаем его черным цветом
            GraphicsDevice.Clear(Color.Black);
            //открываем инструмент для рисования картинок и текста
            _spriteBatch.Begin();

            //проверяем в каком состоянии сейчас игра, чтобы нарисовать нужный экран
            switch (_currentState)
            {
                //если мы в главном меню
                case GameState.MainMenu:
                    //рисуем старт весь экран
                    _spriteBatch.Draw(_startMenu, new Rectangle(0, 0, 1376, 768), Color.White);
                    break;

                //если мы играем
                case GameState.CardSwiping:
                    //рисуем обычный фон
                    _spriteBatch.Draw(_gameBackground, new Rectangle(0, 0, 1376, 768), Color.White);

                    //размеры и центр экрана для карточки
                    int cardWidth = 411;
                    int cardHeight = 702;
                    int cardX = (1376 - cardWidth) / 2;
                    int cardY = (768 - cardHeight) / 2;

                    //рисуем саму рубашку карты
                    _spriteBatch.Draw(_cardTexture, new Rectangle(cardX, cardY, cardWidth, cardHeight), Color.White);

                    //имя и прозвище князя с полупрозрачным фоном
                    if (_gameFont != null)
                    {
                        string fullName = _playerName;
                        if (!string.IsNullOrEmpty(_playerNickname))
                            fullName = "В народе:" + " " + fullName + " " + _playerNickname;

                        Vector2 nameSize = _gameFont.MeasureString(fullName);
                        float nameScale = 0.8f;
                        Vector2 scaledNameSize = nameSize * nameScale;

                        //координаты для текста имени
                        Vector2 namePos = new Vector2(cardX - 360, cardY + 580);

                        //полупрозрачный черный прямоугольник под именем
                        int paddingX = 20;
                        int paddingY = 10;
                        Rectangle nameBgRect = new Rectangle(
                            (int)namePos.X - paddingX,
                            (int)namePos.Y - paddingY,
                            (int)scaledNameSize.X + paddingX * 2,
                            (int)scaledNameSize.Y + paddingY * 2
                        );

                        //рисуем полупрозрачную черную подложку
                        _spriteBatch.Draw(_darkOverlay, nameBgRect, new Color(0, 0, 0, 200));

                        //рисуем текст имени поверх
                        _spriteBatch.DrawString(_gameFont, fullName, namePos, Color.Gold, 0f, Vector2.Zero, nameScale, SpriteEffects.None, 0f);

                        //счетчик лет
                        string yearsText = $"Правит {_yearsPassed} год";
                        if (_yearsPassed % 10 >= 2 && _yearsPassed % 10 <= 4 && (_yearsPassed % 100 < 10 || _yearsPassed % 100 >= 20))
                            yearsText = $"Правит {_yearsPassed} года";
                        else if (_yearsPassed % 10 >= 5 || _yearsPassed % 10 == 0 || (_yearsPassed % 100 >= 11 && _yearsPassed % 100 <= 19))
                            yearsText = $"Правит {_yearsPassed} лет";

                        Vector2 yearsSize = _gameFont.MeasureString(yearsText);
                        float yearsScale = 0.7f;
                        Vector2 yearsPos = new Vector2(cardX - 360, cardY + 630);

                        //фон для счётчика лет
                        Rectangle yearsBgRect = new Rectangle(
                            (int)yearsPos.X - paddingX,
                            (int)yearsPos.Y - paddingY,
                            (int)(yearsSize.X * yearsScale) + paddingX * 2,
                            (int)(yearsSize.Y * yearsScale) + paddingY * 2
                        );
                        _spriteBatch.Draw(_darkOverlay, yearsBgRect, new Color(0, 0, 0, 200));
                        _spriteBatch.DrawString(_gameFont, yearsText, yearsPos, Color.White, 0f, Vector2.Zero, yearsScale, SpriteEffects.None, 0f);
                    }

                    //высчитываем размеры и отступы для 4 иконок характеристик
                    int iconWidth = 59;
                    int iconHeight = 86;
                    int spacing = 30;
                    int iconsRowWidth = iconWidth * 4 + spacing * 3;
                    int startX = cardX + (cardWidth - iconsRowWidth) / 2;
                    int iconY = cardY + 8;

                    //находим координаты по иксу для каждой
                    int x1 = startX;
                    int x2 = x1 + iconWidth + spacing;
                    int x3 = x2 + iconWidth + spacing;
                    int x4 = x3 + iconWidth + spacing;

                    //формула от ИИ
                    //рисуем шкалу веры (серый фон, белая заливка уровня стата и сама иконка сверху)
                    _spriteBatch.Draw(_whitePixel, new Rectangle(x1, iconY, iconWidth, iconHeight), Color.Gray);
                    int fillH1 = (int)(iconHeight * Math.Clamp(_religionValue / 100f, 0, 1));
                    int fillY1 = iconY + (iconHeight - fillH1);
                    _spriteBatch.Draw(_whitePixel, new Rectangle(x1, fillY1, iconWidth, fillH1), Color.White);
                    _spriteBatch.Draw(_religionIcon, new Rectangle(x1, iconY, iconWidth, iconHeight), Color.White);

                    //рисуем шкалу народа
                    _spriteBatch.Draw(_whitePixel, new Rectangle(x2, iconY, iconWidth, iconHeight), Color.Gray);
                    int fillH2 = (int)(iconHeight * Math.Clamp(_peopleValue / 100f, 0, 1));
                    int fillY2 = iconY + (iconHeight - fillH2);
                    _spriteBatch.Draw(_whitePixel, new Rectangle(x2, fillY2, iconWidth, fillH2), Color.White);
                    _spriteBatch.Draw(_peopleIcon, new Rectangle(x2, iconY, iconWidth, iconHeight), Color.White);

                    //рисуем шкалу дружины
                    _spriteBatch.Draw(_whitePixel, new Rectangle(x3, iconY, iconWidth, iconHeight), Color.Gray);
                    int fillH3 = (int)(iconHeight * Math.Clamp(_armyValue / 100f, 0, 1));
                    int fillY3 = iconY + (iconHeight - fillH3);
                    _spriteBatch.Draw(_whitePixel, new Rectangle(x3, fillY3, iconWidth, fillH3), Color.White);
                    _spriteBatch.Draw(_armyIcon, new Rectangle(x3, iconY, iconWidth, iconHeight), Color.White);

                    //рисуем шкалу казны
                    _spriteBatch.Draw(_whitePixel, new Rectangle(x4, iconY, iconWidth, iconHeight), Color.Gray);
                    int fillH4 = (int)(iconHeight * Math.Clamp(_moneyValue / 100f, 0, 1));
                    int fillY4 = iconY + (iconHeight - fillH4);
                    _spriteBatch.Draw(_whitePixel, new Rectangle(x4, fillY4, iconWidth, fillH4), Color.White);
                    _spriteBatch.Draw(_moneyIcon, new Rectangle(x4, iconY, iconWidth, iconHeight), Color.White);

                    //рисуем кружки влияния под иконками (только если портрет наклонен)
                    if (Math.Abs(_currentRotation) > 0.01f && _circleTexture != null && _currentCard != null)
                    {
                        bool isLeft = (_currentRotation < 0f);

                        //смотрим какие статы изменятся от выбора (влево или вправо)
                        float religionChange, peopleChange, armyChange, moneyChange;

                        if (isLeft)
                        {
                            religionChange = _currentCard.LeftReligionChange;
                            peopleChange = _currentCard.LeftPeopleChange;
                            armyChange = _currentCard.LeftArmyChange;
                            moneyChange = _currentCard.LeftMoneyChange;
                        }
                        else
                        {
                            religionChange = _currentCard.RightReligionChange;
                            peopleChange = _currentCard.RightPeopleChange;
                            armyChange = _currentCard.RightArmyChange;
                            moneyChange = _currentCard.RightMoneyChange;
                        }

                        int indicatorY = iconY + 95;

                        //вызываем метод рисования кружков для каждого стата
                        DrawInfluenceCircle(x1 + iconWidth / 2, indicatorY, religionChange);
                        DrawInfluenceCircle(x2 + iconWidth / 2, indicatorY, peopleChange);
                        DrawInfluenceCircle(x3 + iconWidth / 2, indicatorY, armyChange);
                        DrawInfluenceCircle(x4 + iconWidth / 2, indicatorY, moneyChange);
                    }

                    //переносим и центрируем текст вопроса построчно (с помощью ИИ 50/50)
                    if (_gameFont != null)
                    {
                        int maxTextWidth = cardWidth + 30;
                        //разбиваем длинный текст на строчки, чтобы он не вылезал за границы карты
                        string wrappedEventText = WrapText(_gameFont, _currentEventText, maxTextWidth);
                        string[] lines = wrappedEventText.Split('\n');

                        float lineHeight = _gameFont.MeasureString("A").Y * _eventTextScale;
                        float startY = cardY + 120;
                        float centerX = cardX + cardWidth / 2f;

                        //рисуем каждую строчку ровно по центру
                        for (int i = 0; i < lines.Length; i++)
                        {
                            string line = lines[i];
                            Vector2 lineSize = _gameFont.MeasureString(line);
                            Vector2 linePos = new Vector2(centerX, startY + i * lineHeight);
                            Vector2 lineOrigin = new Vector2(lineSize.X / 2f, 0f);

                            _spriteBatch.DrawString(_gameFont, line, linePos, Color.Black, 0f, lineOrigin, _eventTextScale, SpriteEffects.None, 0f);
                        }
                    }

                    //находим центр для портрета персонажа (с учетом смещения)
                    int portraitTopLeftX = cardX + (cardWidth - _portraitWidth) / 2 + (int)_currentDragOffset;
                    int portraitTopLeftY = cardY + 287;
                    Vector2 portraitCenter = new Vector2(portraitTopLeftX + _portraitWidth / 2, portraitTopLeftY + _portraitHeight / 2);

                    //рисуем личико персонажа (с учетом его наклона и размера)
                    _spriteBatch.Draw(_currentPortrait, portraitCenter, null, Color.White, _currentRotation,
                        new Vector2(_currentPortrait.Width / 2f, _currentPortrait.Height / 2f),
                        new Vector2((float)_portraitWidth / _currentPortrait.Width, (float)_portraitHeight / _currentPortrait.Height),
                        SpriteEffects.None, 0f
                    );

                    //рисуем черную плашку и текст ответа поверх портрета при наклоне карты
                    if (Math.Abs(_currentRotation) > 0.01f)
                    {
                        int overlayHeight = 50;
                        int overlayWidth = _portraitWidth - 40;

                        float originYOffset = (_portraitHeight / 2f) - (overlayHeight / 2f + 10);
                        float originXOffset = (_portraitWidth / 2f) - (overlayWidth / 2f) - 20;

                        Vector2 overlayOrigin = new Vector2(0.5f + (originXOffset / overlayWidth), 0.5f + (originYOffset / overlayHeight));

                        //рисуем темную подложку
                        _spriteBatch.Draw(_darkOverlay,
                            portraitCenter,
                            null, Color.White, _currentRotation,
                            overlayOrigin,
                            new Vector2(overlayWidth, overlayHeight),
                            SpriteEffects.None, 0f);

                        //выбираем текст ответа (левый или правый) и рисуем его поверх плашки
                        string choiceText = (_currentRotation < 0f) ? _tempLeftText : _tempRightText;
                        Vector2 textSize = _gameFont.MeasureString(choiceText);
                        Vector2 textOrigin = new Vector2(textSize.X / 2f, (textSize.Y / 2f) + originYOffset + 55);

                        _spriteBatch.DrawString(_gameFont, choiceText,
                            portraitCenter, Color.White, _currentRotation,
                            textOrigin, 0.7f, SpriteEffects.None, 0f);
                    }

                    //имя персонажа в самом низу карточки
                    if (_gameFont != null)
                    {
                        Vector2 nameSize = _gameFont.MeasureString(_characterName);
                        Vector2 namePos = new Vector2(
                            cardX + (cardWidth - nameSize.X) / 2,
                            cardY + cardHeight - 70
                        );
                        _spriteBatch.DrawString(_gameFont, _characterName, namePos, Color.White);
                    }
                    break;

                //если мы проиграли
                case GameState.GameOver:
                    //рисуем экран поражения на весь экран
                    _spriteBatch.Draw(_loseScreen, new Rectangle(0, 0, 1376, 768), Color.White);

                    if (_gameFont != null)
                    {
                        //отображение количества прожитых лет на экране поражения
                        string yearsText = $"Ты правил {_yearsPassed} год";
                        if (_yearsPassed % 10 >= 2 && _yearsPassed % 10 <= 4 && (_yearsPassed % 100 < 10 || _yearsPassed % 100 >= 20))
                            yearsText = $"Ты правил {_yearsPassed} года";
                        else if (_yearsPassed % 10 >= 5 || _yearsPassed % 10 == 0 || (_yearsPassed % 100 >= 11 && _yearsPassed % 100 <= 19))
                            yearsText = $"Ты правил {_yearsPassed} лет";

                        //область для причины смерти (верхняя часть)
                        Rectangle reasonRect = new Rectangle(248, 550, 900, 100);
                        //область для счётчика лет (средняя часть)
                        Rectangle yearsRect = new Rectangle(248, 625, 900, 60);
                        //область для "В народе тебя запомнили как..." (нижняя часть)
                        Rectangle nicknameRect = new Rectangle(248, 660, 900, 80);

                        //вычисляем общую область под все три текста
                        int totalWidth = 900;
                        int totalHeight = 175;
                        int bgX = 248;
                        int bgY = 565;
                        Rectangle bgRect = new Rectangle(bgX, bgY, totalWidth, totalHeight);

                        //рисуем один большой полупрозрачный черный прямоугольник
                        _spriteBatch.Draw(_whitePixel, bgRect, new Color(0, 0, 0, 150));

                        //причина смерти (первая)
                        if (!string.IsNullOrEmpty(_gameOverReason))
                        {
                            DrawTextInRect(_gameOverReason, reasonRect, Color.White, 0.9f);
                        }

                        //счётчик лет (вторая, после причины)
                        DrawTextInRect(yearsText, yearsRect, Color.White, 0.9f);

                        //"В народе тебя запомнили как..." (третья)
                        string fullName = _playerName;
                        if (!string.IsNullOrEmpty(_playerNickname))
                            fullName += " " + _playerNickname;
                        string rememberText = "В народе тебя запомнили как " + fullName;
                        DrawTextInRect(rememberText, nicknameRect, Color.Gold, 0.9f);
                    }
                    break;

                //если мы выиграли
                case GameState.Victory:
                    //рисуем экран победы на весь экран
                    _spriteBatch.Draw(_winScreen, new Rectangle(0, 0, 1376, 768), Color.White);

                    if (_gameFont != null)
                    {
                        //отображение количества прожитых лет на экране победы
                        string yearsText = $"Ты правил {_yearsPassed} год";
                        if (_yearsPassed % 10 >= 2 && _yearsPassed % 10 <= 4 && (_yearsPassed % 100 < 10 || _yearsPassed % 100 >= 20))
                            yearsText = $"Ты правил {_yearsPassed} года";
                        else if (_yearsPassed % 10 >= 5 || _yearsPassed % 10 == 0 || (_yearsPassed % 100 >= 11 && _yearsPassed % 100 <= 19))
                            yearsText = $"Ты правил {_yearsPassed} лет";

                        //прозвище князя
                        string nicknameText = "";
                        if (!string.IsNullOrEmpty(_playerNickname))
                        {
                            nicknameText = $"В народе тебя запомнили как {_playerName} {_playerNickname}";
                        }
                        else
                        {
                            nicknameText = $"В народе тебя запомнили как {_playerName}";
                        }

                        //текст о передаче власти сыну
                        string sonText = "";
                        sonText = $"Бразды правления переданы твоему сыну {_sonName}у";


                        //текст поздравления
                        string victoryText = "Княжество сохранено в целости и сохранности!";

                        //область для поздравления (верхняя часть)
                        Rectangle victoryRect = new Rectangle(248, 545, 900, 80);
                        //область для счётчика лет (вторая)
                        Rectangle yearsRect = new Rectangle(248, 595, 900, 60);
                        //область для прозвища (третья)
                        Rectangle nicknameRect = new Rectangle(248, 630, 900, 80);
                        //область для имени сына (четвёртая)
                        Rectangle sonRect = new Rectangle(248, 670, 900, 80);

                        //вычисляем общую область под все тексты
                        int totalWidth = 900;
                        int totalHeight = 170;
                        int bgX = 248;
                        int bgY = 565;
                        Rectangle bgRect = new Rectangle(bgX, bgY, totalWidth, totalHeight);

                        //рисуем один большой полупрозрачный черный прямоугольник
                        _spriteBatch.Draw(_whitePixel, bgRect, new Color(0, 0, 0, 150));

                        //поздравление (первая)
                        DrawTextInRect(victoryText, victoryRect, Color.Gold, 0.9f);

                        //счётчик лет (вторая)
                        DrawTextInRect(yearsText, yearsRect, Color.White, 0.9f);

                        //прозвище (третья)
                        DrawTextInRect(nicknameText, nicknameRect, Color.White, 0.9f);

                        //имя сына (четвёртая)
                        DrawTextInRect(sonText, sonRect, Color.White, 0.9f);
                    }
                    break;
            }

            //заканчиваем рисование и выводим все это на монитор
            _spriteBatch.End();
            base.Draw(gameTime);
        }

        //метод выбора цвета круга
        private void DrawInfluenceCircle(int centerX, int centerY, float changeValue)
        {
            //смотрим нужно ли рисовать круг
            if (changeValue == 0) return;

            //по дефолту берем базовый белый
            Texture2D textureToDraw = _circleTexture;

            //выбираем цвет в завис от +-
            if (changeValue > 0)
            {
                textureToDraw = _greenCircleTexture;
            }
            else if (changeValue < 0)
            {
                textureToDraw = _redCircleTexture;
            }

            //размер круга
            int circleSize;
            if (Math.Abs(changeValue) > 16)
            {
                circleSize = 22; //ставим большой
            }
            else
            {
                circleSize = 11; //иначе маленький
            }
            //отрисовка в нужном месте (ИИ)
            Rectangle destRect = new Rectangle(
                centerX - circleSize / 2,
                centerY - circleSize / 2,
                circleSize,
                circleSize
            );

            //Рисуем выбранный цветной круг
            _spriteBatch.Draw(textureToDraw, destRect, Color.White);
        }

        //метод переноса строк, написан мной и ИИ 50/50
        private string WrapText(SpriteFont font, string text, float maxLineWidth)
        {
            if (font == null || string.IsNullOrEmpty(text))
                return "";

            string[] words = text.Split(' ');
            StringBuilder sb = new StringBuilder();
            float spaceWidth = font.MeasureString(" ").X;
            float currentLineWidth = 0;

            foreach (string word in words)
            {
                Vector2 size = font.MeasureString(word);

                if (currentLineWidth + size.X > maxLineWidth)
                {
                    if (sb.Length > 0)
                    {
                        sb.Append("\n");
                    }
                    sb.Append(word);
                    currentLineWidth = size.X;
                }
                else
                {
                    if (sb.Length > 0 && sb[sb.Length - 1] != '\n')
                    {
                        sb.Append(" ");
                        currentLineWidth += spaceWidth;
                    }
                    sb.Append(word);
                    currentLineWidth += size.X;
                }
            }

            return sb.ToString();
        }

        //метод сброса анимаций и углов наклона портрета
        private void ResetPortraitState()
        {
            _currentRotation = 0f;
            _targetRotation = 0f;
            _currentDragOffset = 0f;
            _targetDragOffset = 0f;
        }
    }
}