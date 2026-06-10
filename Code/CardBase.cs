using System.Collections.Generic;
using UnificationOfRussianLands;

namespace UnificationOfRussianLands
{
    public static class CardBase
    {
        public static List<Card> CreateDeck()
        {
            List<Card> deck = new List<Card>();

            //-----------------СЮЖЕТНАЯ ЛИНИЯ 1: ЖЕНИТЬБА-------------------
            // Ж1 - Матушка
            deck.Add(new Card
            {
                Id = 1, // 001
                CharacterName = "Матушка",
                PortraitName = "mother",
                Description = "Сын мой, сердце болит за Смоленск. Пора тебе взять жену! Без наследника волки-бояре растащат княжество по кускам.",
                LeftChoiceText = "Зовите сватов, мама",
                RightChoiceText = "Молод еще, подожду",
                RemoveCardsLeft = new List<int> { 11 },
                RemoveCardsRight = new List<int> { },
                RightPeopleChange = -10f,
                RightArmyChange = -10f,
                DelayedCardIdRight = 11,
                DelayCounterRight = 3,
                NextCardIdLeft = 2,
                AddsYear = false
            });
            // Ж1_Повтор - Матушка (Повторная просьба матушки)
            deck.Add(new Card
            {
                Id = 11, // 011
                CharacterName = "Матушка",
                PortraitName = "mother",
                Description = "Княже! Года идут, а терем пуст. Бояре шепчутся, соседи точат мечи. Посмотри невест, не доводи до греха!",
                LeftChoiceText = "Ладно, ведите невест",
                RightChoiceText = "Не бывать браку!",
                RemoveCardsRight = new List<int> { 2, 3, 4, 5, 6, 7 },
                RightPeopleChange = -25f,
                NextCardIdLeft = 2,
                AddsYear = false,
                NicknameRight = "Одинокий"
            });

            // Ж2 - Боярин (Невеста No1)
            deck.Add(new Card
            {
                Id = 2, // 002
                CharacterName = "Боярин",
                PortraitName = "boyar",
                Description = "Княже, посмотри на мою дочерь - красна и умом светла! А приданого даю столько, что сундуки в казне затрещат от злата.",
                LeftChoiceText = "Взять боярышню в жены",
                RightChoiceText = "Ищи другого зятя",
                LeftPeopleChange = 10f,
                LeftMoneyChange = 25f,
                RightPeopleChange = -10f,
                NextCardIdRight = 3,
                NextCardIdLeft = 7,
                RemoveCardsLeft = new List<int> { 3, 4, 5, 6},
                AddsYear = false,
                NicknameLeft = "Скупой"

            });

            // Ж3 - Посол (Невеста No2)
            deck.Add(new Card
            {
                Id = 3, // 003
                CharacterName = "Посол",
                PortraitName = "amb_rus",
                Description = "Мой князь предлагает союз браком. Введи его дочь в терем и наши мечи станут твоими, ни один враг не сунется к нам!",
                LeftChoiceText = "Заключить брак и союз",
                RightChoiceText = "Мне не нужен такой тесть",
                LeftPeopleChange = 10f,
                LeftArmyChange = 25f,
                LeftMoneyChange = -10f,
                RightPeopleChange = -10f,
                RightArmyChange = -10f,
                NextCardIdRight = 4,
                NextCardIdLeft = 7,
                RemoveCardsLeft = new List<int> { 4, 5, 6 },
                AddsYear = false,
                NicknameLeft = "Воинственный"
            });

            // Ж4 - Воевода (Невеста No3)
            deck.Add(new Card
            {
                Id = 4, // 004
                CharacterName = "Воевода",
                PortraitName = "war",
                Description = "Надежа-князь, сестра моя из простой семьи, но весь город любит ее за добрый нрав. Женись, и весь люд станет за тебя горой!",
                LeftChoiceText = "Ввести девицу в терем",
                RightChoiceText = "Женитьба без выгоды?",
                LeftPeopleChange = 25f,
                LeftArmyChange = 10f,
                LeftMoneyChange = -10f,
                RightPeopleChange = -10f,
                RightArmyChange = -10f,
                NextCardIdRight = 5,
                NextCardIdLeft = 7,
                RemoveCardsLeft = new List<int> { 5, 6 },
                AddsYear = false,
                NicknameLeft = "Простой"
            });

            // Ж5 - Византийский посол (Невеста No4)
            deck.Add(new Card
            {
                Id = 5, // 005
                CharacterName = "Византийский посол",
                PortraitName = "amb_viz",
                Description = "Кесарь отдаст тебе в жены знатную гречанку. Но взамен ты уступишь нам торговый путь и будешь платить дань серебром.",
                LeftChoiceText = "Принять условия греков",
                RightChoiceText = "Гнать посла в шею!",
                LeftReligionChange = -10f,
                LeftPeopleChange = -10f,
                LeftMoneyChange = -25f,
                RightReligionChange = 10f,
                RightPeopleChange = 10f,
                RightArmyChange = 10f,
                DelayedCardIdRight = 6,
                DelayCounterRight = 2,
                RemoveCardsLeft = new List<int> { 6 },
                NextCardIdLeft = 7,
                NextCardIdRight = 6,
                AddsYear = false,
                NicknameRight = "Привереда"
            });

            // Ж6 - Вторжение Византии (Воевода)
            deck.Add(new Card
            {
                Id = 6, // 006
                CharacterName = "Воевода",
                PortraitName = "war",
                Description = "Княже, беда! Оскорбленный кесарь нанял степную орду. Они жгут наши рубежи и идут на Смоленск! Дружина ждет приказа.",
                LeftChoiceText = "Вступить в бой",
                RightChoiceText = "Попытаться откупиться",
                LeftPeopleChange = -10f,
                LeftArmyChange = -25f,
                LeftMoneyChange = -25f,
                RightPeopleChange = -10f,
                RightArmyChange = -10f,
                RightMoneyChange = -25f,
                AddsYear = false,
                NicknameRight = "Ошпаренный"

            });

            // Ж7 - Свадьба (Финал ветки женитьбы)
            deck.Add(new Card
            {
                Id = 7,
                CharacterName = "Матушка",
                PortraitName = "mother",
                Description = "Терем сияет, колокола поют! Свадьба удалась на славу, народ гуляет третий день. Ты счастлив, княже?",
                LeftChoiceText = "Да, пируем дальше!",
                RightChoiceText = "Пора за работу!",
                LeftPeopleChange = 10f,
                LeftMoneyChange = -10f,
                DelayedCardIdRight = 101,
                DelayCounterRight = 2,
                DelayedCardIdLeft = 101,
                AddsYear = false,
                DelayCounterLeft = 2,
            });

            //------------СЮЖЕТНАЯ ЛИНИЯ 2: НАСЛЕДНИК И БОЛЕЗНЬ------------

            // Н1 - Рождение сына (Советник)
            deck.Add(new Card
            {
                Id = 101,
                CharacterName = "Советник",
                PortraitName = "smart",
                Description = "Радость, князь! Княгиня родила здорового сына! Наследник у Смоленска! Народ ликует, бояре везут дары. Как наречем младенца?",
                LeftChoiceText = "Назвать Ярополком",
                RightChoiceText = "Назвать Иваном",
                LeftPeopleChange = 10f,
                LeftArmyChange = 10f,
                RightReligionChange = 10f,
                RightPeopleChange = 10f,
                NextCardIdRight=102,
                AddsYear = false,
                NextCardIdLeft =102
            });

            // БЖ1 - Лихорадка (Советник)
            deck.Add(new Card
            {
                Id = 102,
                CharacterName = "Советник",
                PortraitName = "smart",
                Description = "Княже, беда. Молодая княгиня после родов слегла с тяжелой лихорадкой. Знахари лишь руками разводят. Что делать будем?",
                LeftChoiceText = "Позвать лекаря из-за моря",
                RightChoiceText = "Лечиться своими силами",
                LeftReligionChange = -10f,
                LeftPeopleChange = 10f,
                LeftMoneyChange = -25f,
                RightReligionChange = 10f,
                RemoveCardsLeft = new List<int> { 103, 104, 105 },
                NextCardIdRight = 103,
                AddsYear = false,
                NextCardIdLeft =106

            });

            // БЖ2 - Выбор метода (Воевода)
            deck.Add(new Card
            {
                Id = 103,
                CharacterName = "Воевода",
                PortraitName = "war",
                Description = "Княгиня совсем бледна. Звать ли волхвов с их древними заговорами, или послать воинов искать знающих травников среди народа?",
                LeftChoiceText = "Доверить жену волхвам",
                RightChoiceText = "Искать лекаря в народе",
                LeftReligionChange = 10f,
                LeftPeopleChange = 10f,
                LeftMoneyChange = -10f,
                RightReligionChange = -10f,
                RightPeopleChange = 10f,
                RemoveCardsLeft = new List<int> { 105 },
                NextCardIdLeft = 104,
                RemoveCardsRight = new List<int> { 104 },
                AddsYear = false,
                NextCardIdRight = 105
            });

            // БЖ3 - Обряд жрецов (Волхв)
            deck.Add(new Card
            {
                Id = 104,
                CharacterName = "Волхв",
                PortraitName = "religion",
                Description = "Не бойся, князь. Мы окурили терем травами и принесли жертву богам от злых духов. Ее жизнь в руках неба. Жди утра.",
                LeftChoiceText = "Уповать на волю богов",
                RightChoiceText = "Уповать на волю богов",
                LeftReligionChange = 10f,
                LeftPeopleChange = 10f,
                LeftMoneyChange = -10f,
                RightReligionChange = 10f,
                RightPeopleChange = 10f,
                AddsYear = false,
                RightMoneyChange = -10f,
            });

            // БЖ4 - Два лекаря (Советник)
            deck.Add(new Card
            {
                Id = 105,
                CharacterName = "Советник",
                PortraitName = "smart",
                Description = "Привели двоих. Старый травник просит кошель серебра, но лечит полжизни. Молодой ученик просит медяк и клянется, что знает травы.",
                LeftChoiceText = "Нанять старого мастера",
                RightChoiceText = "Довериться ученику",
                LeftMoneyChange = -25f,
                AddsYear = false,
                RightMoneyChange = -10f

            });

            // БЖ5 - Выздоровление княгини (Матушка)
            deck.Add(new Card
            {
                Id = 106,
                CharacterName = "Матушка",
                PortraitName = "mother",
                Description = "Слава небу, княже! Лихорадка отступила. Княгиня поднялась с постели. Весь Смоленск празднует её спасение!",
                LeftChoiceText = "Устроить пир",
                RightChoiceText = "Скромно отметить",
                LeftPeopleChange = 25f,
                LeftArmyChange = 10f,
                LeftMoneyChange = -10f,
                RightPeopleChange = 10f,
                DelayedCardIdLeft=201,
                DelayCounterLeft=4,
                DelayedCardIdRight = 201,
                AddsYear = false,
                DelayCounterRight = 5,

            });

            // БЖ6 - Смерть княгини (Советник)
            deck.Add(new Card
            {
                Id = 107,
                CharacterName = "Советник",
                PortraitName = "smart",
                Description = "Княже, крепись... Скорбная весть. Спасти её не удалось... Весь город облачился в траур.",
                LeftChoiceText = "Проводим её достойно",
                RightChoiceText = "Закрыться в тереме...",
                LeftReligionChange = 10f,
                LeftArmyChange = 10f,
                LeftPeopleChange = 10f,
                LeftMoneyChange = -25f,
                RightPeopleChange = -10f,
                RightMoneyChange = -10f,
                DelayedCardIdLeft = 201,
                DelayCounterLeft = 3,
                AddsYear = false,
                DelayedCardIdRight = 201,
                DelayCounterRight = 4,
                NicknameRight = "Скорбящий",
                NicknameLeft = "Тёмный"
            });


            // ========== СЮЖЕТНАЯ ЛИНИЯ 3: КУЗНЕЦ ==========

            // К1 - Начало (Встреча с мастером)
            deck.Add(new Card
            {
                Id = 201,
                CharacterName = "Кузнец",
                PortraitName = "man",
                Description = "Я выковал меч невиданной прочности, княже! Возьми меня в мастера.\n(Бояре беснуют: Прочь мужика!..)",
                LeftChoiceText = "Взять на службу",
                RightChoiceText = "Пусть кует плуги",
                LeftPeopleChange = 25f,
                LeftArmyChange = 10f,
                LeftMoneyChange = -10f,
                RightPeopleChange = -10f,
                RightArmyChange = -10f,
                // Если отказали кузнецу - вся последующая ветка удаляется
                RemoveCardsRight = new List<int> { 202, 203, 204, 205, 206 },
                AddsYear = false,
                NextCardIdLeft =202
            });

            // К2 - Просьба о ресурсах
            deck.Add(new Card
            {
                Id = 202,
                CharacterName = "Кузнец",
                PortraitName = "man",
                Description = "Княже, задумал я сковать для тебя меч, какого еще не видел свет! Но мне нужно лучшее железо и серебро из казны.",
                LeftChoiceText = "Выдать серебро",
                RightChoiceText = "Куй из простого",
                LeftArmyChange = 10f,
                LeftMoneyChange = -25f,
                RightArmyChange = -10f,
                RightMoneyChange = 10f,
                DelayedCardIdRight = 203,
                DelayCounterRight = 4,
                DelayedCardIdLeft = 203,
                AddsYear = false,
                DelayCounterLeft = 2
            });

            // К3 - Переломный момент
            deck.Add(new Card
            {
                Id = 203,
                CharacterName = "Кузнец",
                PortraitName = "man",
                Description = "Шедевр готов, княже! Года без дневного света... И вот! Меч лично для тебя.",
                LeftChoiceText = "Осмотреть меч",
                RightChoiceText = "Славное оружие!",
                NextCardIdLeft=204,
                NextCardIdRight = 204
            });

            // К3_Купец - Искушение золотом
            deck.Add(new Card
            {
                Id = 204,
                CharacterName = "Купец",
                PortraitName = "market",
                Description = "(Купец рядом жадно осматривает клинок и шепчет):\nЗаморский князь отвалит за это орудие гору золота!",
                LeftChoiceText = "Прочь, окаянный!",
                RightChoiceText = "Продать орудие",
                LeftPeopleChange = 10f,
                LeftArmyChange = 10f,
                LeftMoneyChange = -10f,
                RightPeopleChange = -10f,
                RightMoneyChange = 25f,
                // Если прогнали купца - удаляем карточки возмездия кузнеца
                RemoveCardsLeft = new List<int> { 205, 206 },
                NextCardIdRight = 205,
                NicknameLeft = "Честный",
                AddsYear = false,
                NicknameRight = "Алчный"
            });

            // К4_Нападение - Внезапная угроза
            deck.Add(new Card
            {
                Id = 205,
                CharacterName = "Кузнец",
                PortraitName = "man",
                Description = "Ты продал мой лучший труд за грязное золото?! Смерть торгашу, а не князю!",
                LeftChoiceText = "Что?! Как смеешь?!",
                RightChoiceText = "Стража! Ко мне!",
                NextCardIdLeft = 206,
                AddsYear = false,
                NextCardIdRight = 206
            });

            // К5_Смерть - Неизбежная расплата
            deck.Add(new Card
            {
                Id = 206,
                CharacterName = "Кузнец",
                PortraitName = "man",
                Description = "Стража не успевает.\nОбезумевший мастер делает выпад и пронзает тебя твоим же подарком...",
                LeftChoiceText = "Упасть замертво",
                RightChoiceText = "Упасть замертво",
                InstantDeathLeft = true,
                InstantDeathRight = true,
                AddsYear = false,
                DeathReason = "Вы предали своего мастера-кузнеца и были заколоты собственным великим мечом."
            });


            //-----------СЛУЧАЙНЫЕ КАРТОЧКИ--------------

            // С1 - Налоги (Люд)
            deck.Add(new Card
            {
                Id = 301,
                CharacterName = "Люд",
                PortraitName = "people",
                Description = "Княже, недород в этом году, мошка побила посевы! Снизь подати, иначе наши избы по миру пойдут! Пропадем!",
                LeftChoiceText = "Собрать меньше податей",
                RightChoiceText = "Плетью их! Платить в срок!",
                LeftPeopleChange = 10f,
                LeftMoneyChange = -25f,
                RightPeopleChange = -25f,
                RightMoneyChange = 25f
            });

            // С2 - Беглые слуги (Боярин)
            deck.Add(new Card
            {
                Id = 302,
                CharacterName = "Боярин",
                PortraitName = "boyar",
                Description = "Твои люди укрывают в городе моих беглых слуг! Выдай их на расправу, князь, иначе нарушишь закон и правду!",
                LeftChoiceText = "Выдать беглецов хозяину",
                RightChoiceText = "Оставить беглых в городе",
                LeftPeopleChange = -25f,
                LeftMoneyChange = 10f,
                RightPeopleChange = 10f,
                RightMoneyChange = -10f
            });

            // С3 - Дружинный пир (Воевода)
            deck.Add(new Card
            {
                Id = 303,
                CharacterName = "Воевода",
                PortraitName = "war",
                Description = "Воины твои застоялись без дела. Оружие тупится, чаши пустеют. Закати пир на всю дружину, подними дух ратникам!",
                LeftChoiceText = "Устроить пир горой!",
                RightChoiceText = "Обойдутся без пьянства",
                LeftPeopleChange = 10f,
                LeftArmyChange = 25f,
                LeftMoneyChange = -25f,
                RightArmyChange = -25f,
                RightMoneyChange = 10f
            });

            // С4 - Осквернение святыни (Волхв)
            deck.Add(new Card
            {
                Id = 304,
                CharacterName = "Волхв",
                PortraitName = "religion",
                Description = "Чужеземные купцы устроили попойку возле священного дуба и срубили ветвь! Требуем выкуп серебром на очищение места!",
                LeftChoiceText = "Взыскать золото и наказать",
                RightChoiceText = "Не губите торговлю",
                LeftReligionChange = 25f,
                LeftPeopleChange = 10f,
                LeftMoneyChange = -10f,
                RightReligionChange = -25f,
                RightPeopleChange = -10f,
                RightMoneyChange = 10f
            });

            // С5 - Заморский караван (Купец)
            deck.Add(new Card
            {
                Id = 305,
                CharacterName = "Купец",
                PortraitName = "market",
                Description = "Княже, везем товар по Днепру. Дай нам охрану из своих воинов до самых опасных мест, а мы отсыплем серебра в твою казну.",
                LeftChoiceText = "Выделить воинов охраны",
                RightChoiceText = "Пусть плывут сами",
                LeftArmyChange = -10f,
                LeftMoneyChange = 25f,
                RightArmyChange = 25f,
                RightMoneyChange = -10f
            });

            // С6 - Великая сушь (Волхв)
            deck.Add(new Card
            {
                Id = 306,
                CharacterName = "Волхв",
                PortraitName = "religion",
                Description = "Боги закрыли небо, засуха погубит урожай. Нужно раздать зерно из княжьих запасов голодным и устроить молебен у реки.",
                LeftChoiceText = "Раздать зерно и молиться",
                RightChoiceText = "Беречь припасы для войска",
                LeftReligionChange = 25f,
                LeftPeopleChange = 25f,
                LeftMoneyChange = -25f,
                RightReligionChange = -25f,
                RightPeopleChange = -25f,
                RightArmyChange = 10f,
                RightMoneyChange = 25f
            });

            // С7 - Пограничный спор (Посол)
            deck.Add(new Card
            {
                Id = 307,
                CharacterName = "Посол",
                PortraitName = "amb_rus",
                Description = "Наши деды в этих лесах мёд собирали! Уступи земли добром, княже, или быть большой войне.",
                LeftChoiceText = "Отдать леса ради мира",
                RightChoiceText = "Послать отряд в леса",
                LeftPeopleChange = -10f,
                LeftArmyChange = -10f,
                LeftMoneyChange = -10f,
                RightPeopleChange = 10f,
                RightArmyChange = 10f,
                RightMoneyChange = -25f
            });

            // С8 - Лихие люди (Воевода)
            deck.Add(new Card
            {
                Id = 308,
                CharacterName = "Воевода",
                PortraitName = "war",
                Description = "В лесах объявилась банда разбойников - грабят деревни, режут скот. Позволь выжечь их логово, но придется попотеть.",
                LeftChoiceText = "Истребить разбойников",
                RightChoiceText = "Пусть люди защищаются сами",
                LeftPeopleChange = 25f,
                LeftArmyChange = -10f,
                LeftMoneyChange = -10f,
                RightPeopleChange = -25f,
                RightArmyChange = 10f
            });

            // С9 - Пожар в городе (Люд)
            deck.Add(new Card
            {
                Id = 309,
                CharacterName = "Люд",
                PortraitName = "people",
                Description = "Горит ремесленный посад! Сухой ветер гонит огонь прямо на склады. Княже, спасай город, высылай дружину тушить!",
                LeftChoiceText = "Отправить воинов тушить",
                RightChoiceText = "Охранять терем и казну",
                LeftPeopleChange = 25f,
                LeftArmyChange = -10f,
                LeftMoneyChange = -10f,
                RightPeopleChange = -25f,
                RightArmyChange = 10f,
                RightMoneyChange = 10f
            });

            // С10 - Дерзкая кража (Купец)
            deck.Add(new Card
            {
                Id = 310,
                CharacterName = "Купец",
                PortraitName = "market",
                Description = "Ночью из моих лавок украли дорогие меха! Следы ведут к местным крестьянам. Позволь устроить обыск и поймать воров.",
                LeftChoiceText = "Разрешить обыск у крестьян",
                RightChoiceText = "Не чинить самосуд",
                LeftPeopleChange = -10f,
                LeftMoneyChange = -10f,
                RightPeopleChange = 10f,
                RightMoneyChange = 10f
            });

            // С11 - Жертва богам (Волхв)
            deck.Add(new Card
            {
                Id = 311,
                CharacterName = "Volkhv",
                PortraitName = "religion",
                Description = "Грядет зима, волки лютуют. Чтобы задобрить богов, нужно принести в жертву лучшего коня из княжеской конюшни!",
                LeftChoiceText = "Отдать коня богам",
                RightChoiceText = "Идолам хватит и быка",
                LeftReligionChange = 25f,
                LeftPeopleChange = 10f,
                LeftArmyChange = -10f,
                LeftMoneyChange = -10f,
                RightReligionChange = -25f,
                RightPeopleChange = -10f,
                RightArmyChange = 10f
            });

            // С12 - Народное собрание (Люд)
            deck.Add(new Card
            {
                Id = 312,
                CharacterName = "Люд",
                PortraitName = "people",
                Description = "На площади забил вечевой колокол. Народ требует убрать назначенного тобой судью - он вор и кривдой судит!",
                LeftChoiceText = "Сменять судью",
                RightChoiceText = "Разогнать вече силой!",
                LeftPeopleChange = 25f,
                LeftArmyChange = -10f,
                LeftMoneyChange = -10f,
                RightReligionChange = -10f,
                RightPeopleChange = -25f,
                RightArmyChange = 25f,
                NicknameRight = "Каратель"
            });

            // С13 - Бродячие артисты (Волхв)
            deck.Add(new Card
            {
                Id = 313,
                CharacterName = "Волхв",
                PortraitName = "religion",
                Description = "В город пришли бродячие скоморохи. Они смущают народ и чернят богов глупыми песнями, выдвори их!",
                LeftChoiceText = "Пусть потешать народ",
                RightChoiceText = "Прогнать их из княжества",
                LeftReligionChange = -25f,
                LeftPeopleChange = 25f,
                LeftMoneyChange = 10f,
                RightReligionChange = 25f,
                RightPeopleChange = -25f
            });

            // С14 - Княжий суд (Боярин)
            deck.Add(new Card
            {
                Id = 314,
                CharacterName = "Боярин",
                PortraitName = "boyar",
                Description = "Сосед-купец занял у меня золото и прячет товар, отдавать не хочет! Позволь моим слугам силой забрать весь долг.",
                LeftChoiceText = "Взять долг силой",
                RightChoiceText = "Судить строго по правде",
                LeftPeopleChange = -10f,
                LeftMoneyChange = 25f,
                RightPeopleChange = 10f,
                RightMoneyChange = -10f
            });

            // С15 - Загадочный лекарь (Воевода)
            deck.Add(new Card
            {
                Id = 315,
                CharacterName = "Воевода",
                PortraitName = "war",
                Description = "Этот лекарь приплыл из дальних стран. Предлагает за золото осмотреть раненых ратников, но наши знахари плюют ему вслед.",
                LeftChoiceText = "Пусть лечит воинов",
                RightChoiceText = "Выставить чужеземца",
                LeftReligionChange = -10f,
                LeftArmyChange = 25f,
                LeftMoneyChange = -10f,
                RightReligionChange = 10f,
                RightArmyChange = -25f
            });

            // С16 - Княжья охота (Воевода)
            deck.Add(new Card
            {
                Id = 316,
                CharacterName = "Воевода",
                PortraitName = "war",
                Description = "В лесах мы наткнулись на медведя-людоеда. Зверь задрал двух коней. Объявим большую охоту или оставим всё как есть?",
                LeftChoiceText = "Собрать отряд и убить зверя",
                RightChoiceText = "Не тратить время на зверя",
                LeftPeopleChange = 10f,
                LeftArmyChange = 10f,
                LeftMoneyChange = -10f,
                RightPeopleChange = -25f,
                RightArmyChange = -10f,
                RightMoneyChange = 10f
            });

            // С17 - Старый долг (Посол)
            deck.Add(new Card
            {
                Id = 317,
                CharacterName = "Посол",
                PortraitName = "amb_rus",
                Description = "Твой отец занимал у нас золото. Срок вышел. Отдавай серебром или пришли тридцать лучших воинов в наше войско.",
                LeftChoiceText = "Вернуть долг серебром",
                RightChoiceText = "Отправить воинов на службу",
                LeftArmyChange = 10f,
                LeftMoneyChange = -25f,
                RightArmyChange = -25f,
                RightMoneyChange = 10f
            });

            // С18 - Волчья стая (Волхв)
            deck.Add(new Card
            {
                Id = 318,
                CharacterName = "Волхв",
                PortraitName = "religion",
                Description = "Волки воют у самых ворот города, боги шлют предупреждение! Нужно запереть ворота на три дня и молиться.",
                LeftChoiceText = "Объявить три дня молитв",
                RightChoiceText = "Пусть стража патрулирует",
                LeftReligionChange = 25f,
                LeftPeopleChange = -10f,
                LeftMoneyChange = -10f,
                RightReligionChange = -25f,
                RightPeopleChange = 10f,
                RightArmyChange = 10f
            });

            // С19 - Фальшивые монеты (Купец)
            deck.Add(new Card
            {
                Id = 319,
                CharacterName = "Купец",
                PortraitName = "market",
                Description = "На рынке беда! Пустили в оборот фальшивое серебро со свинцом. Позволь устроить жесткую проверку всех лавок!",
                LeftChoiceText = "Перетряхнуть весь рынок!",
                RightChoiceText = "Пусть купцы ищут вора сами",
                LeftPeopleChange = -25f,
                LeftMoneyChange = 25f,
                RightPeopleChange = 10f,
                RightMoneyChange = -25f
            });

            // С20 - Удачная торговля (Купец)
            deck.Add(new Card
            {
                Id = 320,
                CharacterName = "Купец",
                PortraitName = "market",
                Description = "Княже, торговый караван из далёких земель пришёл в Смоленск. Купцы просят торговать на наших землях.",
                LeftChoiceText = "Разрешить торговать",
                RightChoiceText = "Взять высокую пошлину",
                LeftPeopleChange = 10f,
                LeftMoneyChange = 10f,
                RightPeopleChange = -10f,
                RightMoneyChange = 25f
            });

            // С21 - Щедрый урожай (Люд)
            deck.Add(new Card
            {
                Id = 321,
                CharacterName = "Люд",
                PortraitName = "people",
                Description = "Княже! В этом году урожай удался на славу! Смерды принесли тебе часть зерна и мёда в дар.",
                LeftChoiceText = "Принять дары",
                RightChoiceText = "Устроить пир для народа",
                LeftMoneyChange = 10f,
                RightPeopleChange = 10f,
                RightMoneyChange = -10f
            });

            // С22 - Находка клада (Люд)
            deck.Add(new Card
            {
                Id = 322,
                CharacterName = "Люд",
                PortraitName = "people",
                Description = "Крестьянин нашёл в поле древний клад! Серебряные монеты и украшения. Он принёс их тебе как законному владыке.",
                LeftChoiceText = "Взять клад в казну",
                RightChoiceText = "Оставить находку крестьянину",
                LeftMoneyChange = 10f,
                LeftPeopleChange = -10f,
                RightPeopleChange = 10f
            });

            // С23 - Выгодный заказ (Купец)
            deck.Add(new Card
            {
                Id = 323,
                CharacterName = "Купец",
                PortraitName = "market",
                Description = "Княже, соседний князь хочет купить у нас партию оружия. Цена хорошая, но вооружать соседа - опасно.",
                LeftChoiceText = "Продать оружие",
                RightChoiceText = "Отказать в продаже",
                LeftMoneyChange = 25f,
                LeftArmyChange = -10f,
                RightArmyChange = 10f,
                RightMoneyChange = -10f
            });


            // С24 - Экономия (Советник)
            deck.Add(new Card
            {
                Id = 324,
                CharacterName = "Советник",
                PortraitName = "smart",
                Description = "Княже, я нашёл способ сократить расходы на содержание дружины, не ущемляя воинов.",
                LeftChoiceText = "Согласиться на экономию",
                RightChoiceText = "Отказаться, пусть всё идёт как есть",
                LeftMoneyChange = 25f,
                LeftArmyChange = -10f,
                RightArmyChange = 10f
            });

            //-----------------ПРОЛОГ (СМЕРТЬ ОТЦА, РЕГЕНТ, СВЕРЖЕНИЕ)-------------------

            // П0 - Вступление (Летописец)
            deck.Add(new Card
            {
                Id = 50,
                CharacterName = "Летописец",
                PortraitName = "writer",
                Description = "В лето 6575-е от сотворения мира преставился князь великий Святослав, батюшка твой, убиен на реке...",
                LeftChoiceText = "Листать дальше",
                RightChoiceText = "Листать дальше",
                NextCardIdLeft = 51,
                AddsYear = false,
                NextCardIdRight = 51
            });

            // П1 - Смерть отца (Летописец)
            deck.Add(new Card
            {
                Id = 51,
                CharacterName = "Летописец",
                PortraitName = "writer",
                Description = "Тебе тогда минуло двенадцать. Отец ушёл в поход и не вернулся. Гонец принёс страшную весть в слезах.",
                LeftChoiceText = "Что дальше?",
                RightChoiceText = "Что дальше?",
                NextCardIdLeft = 52,
                AddsYear = false,
                NextCardIdRight = 52
            });

            // П2 - Регентство (Летописец)
            deck.Add(new Card
            {
                Id = 52,
                CharacterName = "Летописец",
                PortraitName = "writer",
                Description = "Бояре посадили на трон регента - боярина Вельямина. 'Молод княжич, не время ему править', - говорили они.",
                LeftChoiceText = "Шесть лет спустя...",
                RightChoiceText = "Шесть лет спустя...",
                NextCardIdLeft = 53,
                AddsYear = false,
                NextCardIdRight = 53
            });

            // П3_1 - Предыстория регентства
            deck.Add(new Card
            {
                Id = 53,
                CharacterName = "Летописец",
                PortraitName = "writer",
                Description = "Шесть долгих лет правил регент Вельямин. Он распустил дружину, разорил казну и чуть не отдал Смоленск врагам.",
                LeftChoiceText = "Но время пришло...",
                RightChoiceText = "Пора вернуть свое...",
                NextCardIdLeft = 54,
                AddsYear = false,
                NextCardIdRight = 54
            });

            // П3_2 - Поимка регента и суд
            deck.Add(new Card
            {
                Id = 54,
                CharacterName = "Летописец",
                PortraitName = "writer",
                Description = "Сегодня твои верные люди ворвались в покои предателя. Вельямин пойман, и судьба его в твоих руках. Какова его участь?",
                LeftChoiceText = "Казнить предателя!",
                RightChoiceText = "Прогнать гада!",
                // Последствия выбора
                LeftPeopleChange = 10f,   // Народ ликует от суровой справедливости
                LeftArmyChange = 10f,     // Дружина уважает жесткую руку
                RightReligionChange = 10f, // Волхвы одобряют милосердие
                RightPeopleChange = -10f,  // Народ недоволен, что гад остался жив
                NextCardIdLeft = 1,
                AddsYear = false,
                NextCardIdRight = 1
            });
            // К0_1 - Учим свайпать влево и вправо
            deck.Add(new Card
            {
                Id = 100,
                CharacterName = "Летописец",
                PortraitName = "writer",
                Description = "Княже! Судьба Руси - в твоих решениях. Потяни карточку ВЛЕВО или ВПРАВО, чтобы сделать свой первый выбор.",
                LeftChoiceText = "Потянуть влево...",
                RightChoiceText = "Потянуть вправо...",
                NextCardIdLeft = 1001,
                AddsYear = false,
                NextCardIdRight = 1002
            });

            // К0_2_А - Если игрок потянул влево (Объясняем правила)
            deck.Add(new Card
            {
                Id = 1001,
                CharacterName = "Летописец",
                PortraitName = "writer",
                Description = "Гляди на шкалы наверху! Твоя задача - держать баланс. Если хоть один показатель упадет до нуля или заполнится до конца - погибель!",
                LeftChoiceText = "Я всё понял",
                RightChoiceText = "Начать игру",
                NextCardIdLeft = 50,
                AddsYear = false,
                NextCardIdRight = 50
            });

            // К0_2_Б - Если игрок потянул вправо (Объясняем правила, чуть другой текст для интерактива)
            deck.Add(new Card
            {
                Id = 1002,
                CharacterName = "Летописец",
                PortraitName = "writer",
                Description = "Умело орудуешь! Теперь помни: береги баланс фракций наверху. Опустишь шкалу в ноль или переполнишь её - конец!",
                LeftChoiceText = "Я готов править",
                RightChoiceText = "Начать игру",
                NextCardIdLeft = 50,
                AddsYear = false,
                NextCardIdRight = 50
            });
            
            return deck;
        }
    }
}