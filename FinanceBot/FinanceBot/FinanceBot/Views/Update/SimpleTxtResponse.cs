using System;
namespace FinanceBot.Views.Update
{
    public static class SimpleTxtResponse
    {
        //help
        private static string CommandHelp =
            "Добавить расходы (например): 150 обед\n" +
            "Статистика за сегодня: /today\n" +
            "Статистика за месяц: /month\n" +
            "Последние расходы: /expenses\n" +
            "Категории расходов: /categories\n";

        public static string Help =
            "Бот для учета финансов\n\n" +
            "Для начала использования, введите число в диапазоне от 1 " +
            "до 31 - день начисления зарплаты. Таким образом задается " +
            "период для расчета статистики за месяц\n\n" +
            "Добавляйте расходы в формате:\n(сумма) (категория расходов)\n" +
            "Например: 150 обед\n\n" +
            "Бот будет вести статистику в разрезе категорий, а также " +
            "выделять фиксированные (постоянные, ежемесячные) затраты " +
            "(связь/аренда и т.д.).\n\n" +
            "Доступные команды:\n" +
            CommandHelp;

        //start
        public static string HelloNewUser =
            "Бот для учета финансов\n\n" +
            "Введите день начисления зарплаты\n" +
            "/help - справка";

        public static string HelloUser =
            "Бот для учета финансов\n\n" +
            CommandHelp + "\n" +
            "/help - справка";

        //today
        public static string TodayStatistic =
            "Расходы за сегодня\n" +
            "всего - {0} руб." +
            "фиксированные - {1} руб.\n\n" +
            "За текущий месяц: /month";

        //month
        public static string MonthStatistic =
            "Расходы за месяц\n" +
            "всего - {0} руб." +
            "фиксированные - {1} руб.\n\n";

        //добавление расходов
        public static string AddExpense =
            "Добавлены расходы {2}\n\n" +
            TodayStatistic;

        //expenses
        //сумма, категория, описание, команда
        public static string ExpenseTemplate =
            "* {0} руб. - {1}\n{2} ({3} для удаления)\n\n";

        public static string LastExpenses =
            "Последние внесенные расходы:\n\n {0}";

        //categories
        //название, описание, ежемесячная, кастомная
        public static string CategoryTemplate =
            "* {0} ({1} - {2}, {3})";

        public static string Categories =
            "Категории расходов:\n\n {0}";

        //parse commands
        public static string AddCategory =
            "Категория \'{0}\' добавлена!";

        public static string DelCategory =
            "Категория \'{0}\' удалена!";

        public static string ChangeSalaryDay =
            "Изменен период для расчета статистики за месяц! " +
            "Текущий период: {0}.{1} - {2}.{3}";

        //exceptions
        public static string GeneralExceptionInfo =
            "{0} - команда не поддерживается или не существует!";

        public static string UserNotFound =
            "{0}: пользователь с id{1} не зарегистрирован!";

        public static string UserAlredyExist =
            "Пользователь с id{0} уже существует!";

        public static string CategoryNotFound =
            "Категория \'{0}\' не существует!";

        public static string CategoryAlredyExist =
            "Категория \'{0}\' уже существует!";

        public static string BadExpense =
            "\'{0}\' - невозможно определить категорию или сумму расхода!";

        //TODO: команда "дней до зарплаты"
    }
}
