using System;
using System.Collections.Generic;

namespace UnificationOfRussianLands
{
    public class Card
    {
        //1. БАЗА О КАРТОЧКЕ

        //уникальный ID карточки
        public int Id { get; set; }

        //имя персонажа
        public string CharacterName { get; set; }

        //имя файла портрета
        public string PortraitName { get; set; }

        //текст карточки
        public string Description { get; set; }


        //2. ВАРИАНТЫ ВЫБОРА (ВЛЕВО / ВПРАВО)

        public string LeftChoiceText { get; set; }
        public string RightChoiceText { get; set; }


        //3. ИЗМЕНЕНИЕ ХАРАКТЕРИСТИК ПРИ ВЫБОРЕ

        //левый выбор
        public float LeftReligionChange { get; set; }     //Вера
        public float LeftPeopleChange { get; set; }    //Народ
        public float LeftArmyChange { get; set; }      //Дружина
        public float LeftMoneyChange { get; set; }  //Казна

        //правый выбор
        public float RightReligionChange { get; set; }
        public float RightPeopleChange { get; set; }
        public float RightArmyChange { get; set; }
        public float RightMoneyChange { get; set; }

        /*
        //4. УСЛОВИЯ ПОЯВЛЕНИЯ КАРТЫ (ТРИГГЕРЫ)

        //нижний порог (карта появится, ТОЛЬКО если текущее значение >= указанному)
        public float MinReligion { get; set; } = 0f;
        public float MinPeople { get; set; } = 0f;
        public float MinArmy { get; set; } = 0f;
        public float MinMoney { get; set; } = 0f;

        //верхний порог (карта появится, ТОЛЬКО если текущее значение <= указанному)
        public float MaxReligion { get; set; } = 100f;
        public float MaxPeople { get; set; } = 100f;
        public float MaxArmy { get; set; } = 100f;
        public float MaxMoney { get; set; } = 100f;


        //список уникальных строк-идентификаторов персонажей, которые ОБЯЗАНЫ быть ЖИВЫ,
        //чтобы эта карта могла выпасть. Если хоть один мертв, то карта скип
        public List<string> RequiredLivingCharacters { get; set; } = new List<string>();

        */

        //5. СЮЖЕТНАЯ ЛОГИКА И ЦЕПОЧКИ КВЕСТОВ (ЛОГИКА ЧЕРЕЗ УДАЛЕНИЕ КАРТ)


        //список ID карт, которые гарантированно и сразу УДАЛЯТСЯ из колоды при свайпе ВЛЕВО
        //для динамического изменения и отмены сюжетных веток
        public List<int> RemoveCardsLeft { get; set; } = new List<int>();


        //список ID карт, которые гарантированно и сразу УДАЛЯТСЯ из колоды при свайпе ВПРАВО
        public List<int> RemoveCardsRight { get; set; } = new List<int>();

        // ХРАНИЛИЩЕ ДЛЯ СЛЕДУЮЩЕЙ КАРТЫ (0 если следующая карта случайная)
        public int NextCardIdLeft { get; set; } = 0;
        public int NextCardIdRight { get; set; } = 0;

        //6. СЮЖЕТНЫЕ СМЕРТИ И ГЛОБАЛЬНЫЕ ФЛАГИ


        //имя персонажа, который дохнет-если влево
        public string KillsCharacterLeft { get; set; } = "";


        //имя персонажа, который дохнет-если вправо
        public string KillsCharacterRight { get; set; } = "";

        //описание ниже написал с моих слов ИИ, я не смог по умному написать
        //Текстовый флаг (достижение / триггер), который активируется при выборе ВЛЕВО.
        //Используется для карт спасения или проверки условий в будущем.
        public string ClearFlagLeft { get; set; } = "";

        //Текстовый флаг (достижение / триггер), который активируется при выборе ВПРАВО.
        public string ClearFlagRight { get; set; } = "";



        //7. МГНОВЕННАЯ СМЕРТЬ И КОНЕЦ ИГРЫ


        //левый выбор-мгновенный луз?
        public bool InstantDeathLeft { get; set; } = false;


        //правый выбор-мгновенный луз?
        public bool InstantDeathRight { get; set; } = false;


        //специальный текст причины смерти
        //если игрок погиб мгновенно от этой карты
        public string DeathReason { get; set; } = "";


        // 8. ОТЛОЖЕННОЕ ПОЯВЛЕНИЕ КАРТОЧКИ (раздельно для левого и правого выбора)

        // ID карточки, которая появится после задержки при выборе ЛЕВОГО варианта
        public int DelayedCardIdLeft { get; set; } = 0;

        // Через сколько ходов появится при выборе ЛЕВОГО варианта
        public int DelayCounterLeft { get; set; } = 0;

        // ID карточки, которая появится после задержки при выборе ПРАВОГО варианта
        public int DelayedCardIdRight { get; set; } = 0;

        // Через сколько ходов появится при выборе ПРАВОГО варианта
        public int DelayCounterRight { get; set; } = 0;


        // 9. СМЕНА ПРОЗВИЩА КНЯЗЯ

        // Новое прозвище при выборе ЛЕВОГО варианта (если не пусто)
        public string NicknameLeft { get; set; } = "";

        // Новое прозвище при выборе ПРАВОГО варианта (если не пусто)
        public string NicknameRight { get; set; } = "";

        // 10. УВЕЛИЧЕНИЕ СЧЁТЧИКА ЛЕТ

        // Добавляет ли эта карточка год к счётчику правления?
        // true = добавляет (случайные события)
        // false = не добавляет (сюжетные карточки)
        public bool AddsYear { get; set; } = true;

        //КОНСТРУКТОР КЛАССА
        public Card()
        {
            //RequiredLivingCharacters = new List<string>();
            NextCardIdRight = 0;
            NextCardIdLeft = 0;
            RemoveCardsLeft = new List<int>();
            RemoveCardsRight = new List<int>();
        }
    }
}