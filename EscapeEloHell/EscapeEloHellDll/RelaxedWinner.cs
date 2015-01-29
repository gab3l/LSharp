using System;
using System.Collections.Generic;
using System.Linq;
using RelaxedWinnerDll.Data;

namespace RelaxedWinnerDll
{
    public class RelaxedWinner
    {
        static RelaxedWinner()
        {
            MessageData = new Messages();
        }

        public static Messages MessageData { get; set; }

        public static bool RepeatMaximum(int minimumWaitTimeInMinutes, List<Information> messages)
        {
            return messages.Any(x => x.DateTime.AddMinutes(minimumWaitTimeInMinutes) < DateTime.Now);
        }

        public static Information GetMessage(IList<Information> messages)
        {
            var unusedMessages = messages.Where(x => !x.IsUsed);

            var myMessages = unusedMessages as IList<Information> ?? unusedMessages.ToList();
            if (AllUsedOnce(myMessages))
            {
                // reset list
                Reset(messages);
                // refill list
                myMessages = messages;
            }

            var index = new Random().Next(0, myMessages.Count());
            var randomMessage = myMessages.ElementAt(index);
            randomMessage.IsUsed = true;
            randomMessage.DateTime = DateTime.Now;
            return randomMessage;
        }

        /// <summary>
        ///     Test function only so far!
        /// </summary>
        public static string GetNewMessage(Func<List<Information>, Information> function, List<Information> myMessages)
        {
            var result = function(myMessages);
            result.IsUsed = true;
            result.DateTime = DateTime.Now;

            return result.Message;
        }

        private static bool AllUsedOnce(IEnumerable<Information> result)
        {
            return !result.Any();
        }

        private static void Reset(IList<Information> messages)
        {
            foreach (var item in messages)
            {
                item.IsUsed = false;
            }
        }
    }
}