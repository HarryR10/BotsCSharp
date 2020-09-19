using System;
using Telegram.Bot.Types;
using FinanceBot.Models.EntityModels;

namespace FinanceBot.Utils
{
    public static class MessageParser
    {
        public static bool ParseExpense(Message message, out Expense expense)
        {
            //проверяем соответствие паттерну
            string regexTemplate = @"^\d+\s.+";


            //делим на сумму, категорию и описание
            //добавляем сегодняшнюю дату

            //если неудачный парсинг
            expense = null;
            return false;
        }
    }
}
