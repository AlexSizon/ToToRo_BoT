using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;

namespace ToToRoBot
{
    class Program
    {
        static TelegramBotClient Bot;
        static long chatId;
        static void Main(string[] args)
        {
            Bot = new TelegramBotClient("1068227873:AAGPwNMJBW8lzPJz2m-5qvhkgAVoPZwnsDI");

            Bot.OnMessage += Bot_OnMessageReceived;

            var me = Bot.GetMeAsync().Result;

            Console.WriteLine(me.FirstName);
            RestartBot(1);
            Bot.StartReceiving();
            Console.ReadLine();
        }

        private static void Bot_OnMessageReceived(object sender, MessageEventArgs e)
        {
            chatId = e.Message.Chat.Id;
            if (e.Message.Type == Telegram.Bot.Types.Enums.MessageType.ChatMembersAdded)
            {
                //Bot.DeleteMessageAsync(chatId, e.Message.MessageId);
                Bot.SendTextMessageAsync(chatId, "Привет " + e.Message.From.FirstName + " " + e.Message.From.LastName + "! Заходи, не стесняйся ✌️ это общий чат онлайн фотокурсов студии RedLab.\n\nЗдесь можно свободно общаться и задавать любые вопросы по фотокурсу и о фотографии в целом.\n\nВся информация о курсе (отзывы, программа, цены, скидки) доступна в [закрепленном сообщении](https://t.me/c/1236126100/4097)", Telegram.Bot.Types.Enums.ParseMode.Markdown);
                DeleteMessagebyTimer(chatId, e.Message);
            }
            else if (e.Message.Type == Telegram.Bot.Types.Enums.MessageType.ChatMemberLeft)
            {
                Bot.DeleteMessageAsync(chatId, e.Message.MessageId);
            }
        }
        private async static void DeleteMessagebyTimer(long chatId, Message e)
        {
            await Task.Run(() => AutomaticMessageAddUserDelete(chatId, e.MessageId + 1));
        }
        private async static void AutomaticMessageAddUserDelete(long chatId, int messageId)
        {
            System.Threading.Thread.Sleep(new TimeSpan(0, 10, 0));
            await Bot.DeleteMessageAsync(chatId, messageId);
        }
        private async static void RestartBot(int hours)
        {
            await Task.Run(() => RestartBotByTimer(hours));
        }
        private static void RestartBotByTimer(int hours)
        {
            System.Threading.Thread.Sleep(new TimeSpan(hours, 0, 0));
            var fileName = Assembly.GetExecutingAssembly().Location;
            System.Diagnostics.Process.Start(fileName);
            Environment.Exit(-1);
        }
    }
}
//Tо́TоRо - маленький миньон студии RedLab
//@ToToRo_RedLab_BoT