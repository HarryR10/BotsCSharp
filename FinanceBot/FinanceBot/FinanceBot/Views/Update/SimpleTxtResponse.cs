using System;
namespace FinanceBot.Views.Update
{
    public static class SimpleTxtResponse
    {
        //help
        private static string CommandHelp { get; } =
            "Добавить расходы (например): 150 обед\n" +
            "Статистика за сегодня: /today\n" +
            "Статистика за месяц: /month\n" +
            "Последние расходы: /expenses\n" +
            "Категории расходов: /categories\n";

        public static string Help { get; } =
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
        public static string HelloNewUser { get; } =
            "Бот для учета финансов\n\n" +
            "Введите день начисления зарплаты\n" +
            "/help - справка";

        public static string HelloUser { get; } =
            "Бот для учета финансов\n\n" +
            CommandHelp + "\n" +
            "/help - справка";

        //today
        public static string TodayStatistic { get; } =
            "Расходы за сегодня\n" +
            "всего - {0} руб." +
            "фиксированные - {1} руб.\n\n" +
            "За текущий месяц: /month";

        //month
        public static string MonthStatistic { get; } =
            "Расходы за месяц\n" +
            "всего - {0} руб." +
            "фиксированные - {1} руб.\n\n";

        //добавление расходов
        public static string AddExpense { get; } =
            "Добавлены расходы {2}\n\n" +
            TodayStatistic;

        //expenses
        //сумма, категория, описание, команда
        public static string ExpenseTemplate { get; } =
            "* {0} руб. - {1}\n{2} ({3} для удаления)\n\n";

        public static string LastExpenses { get; } =
            "Последние внесенные расходы:\n\n {0}";

        //categories
        //название, описание, ежемесячная, кастомная
        public static string CategoryTemplate { get; } =
            "* {0} ({1} - {2}, {3})";

        public static string Categories { get; } =
            "Категории расходов:\n\n {0}";

        //exceptions
        public static string GeneralExceptionInfo { get; } =
            "Ошибка ввода!";
    }
}
