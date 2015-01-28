using System;
using System.Collections.Generic;
using System.Linq;
using FriendlyWinnerDll.Data;

namespace FriendlyWinnerDll
{
    public class FriendlyWinner
    {
        static FriendlyWinner()
        {
            Motivation = new Messages();
        }

        public static Messages Motivation { get; set; }

        public static bool RepeatMaximum(int minutes, List<MyMessage> list)
        {
            return list.Any(x => x.DateTime.AddMinutes(minutes) < DateTime.Now);
        }

        public static MyMessage GetMessage(IList<MyMessage> messages)
        {
            var unusedMessages = messages.Where(x => !x.IsUsed);

            var myMessages = unusedMessages as IList<MyMessage> ?? unusedMessages.ToList();
            if (AllUsedOnce(myMessages))
            {
                Reset(messages);
                myMessages = messages;
            }

            var index = new Random().Next(0, myMessages.Count());
            var result = myMessages.ElementAt(index);
            result.IsUsed = true;
            result.DateTime = DateTime.Now;
            return result;
        }

        /// <summary>
        ///     Helper for Tests
        /// </summary>
        public static string GetNewMessage(Func<List<MyMessage>, MyMessage> function, List<MyMessage> myMessages)
        {
            var result = function(myMessages);
            result.IsUsed = true;
            result.DateTime = DateTime.Now;

            return result.Message;
        }

        private static bool AllUsedOnce(IEnumerable<MyMessage> result)
        {
            return !result.Any();
        }

        private static void Reset(IList<MyMessage> messages)
        {
            foreach (var item in messages)
            {
                item.IsUsed = false;
            }
        }
    }
}