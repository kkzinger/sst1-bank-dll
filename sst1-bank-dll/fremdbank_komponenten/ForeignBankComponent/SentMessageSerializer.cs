using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace ForeignComponent
{
    public class SentMessageSerializer
    {
        private const string fileName = "sent_messages.xml";


        public Dictionary<long, BankMessage.BankMessage> Load()
        {
            var dict = new Dictionary<long, BankMessage.BankMessage>();

            if (!File.Exists(fileName)) // File not yet written
            {
                return dict;
            }

            var serializer = new XmlSerializer(typeof(List<BankMessage.BankMessage>));
            List<BankMessage.BankMessage> messagesList;
            using (TextReader tw = new StreamReader(fileName))
            {
                messagesList = (List<BankMessage.BankMessage>)serializer.Deserialize(tw);
            }
            messagesList.ForEach(x => dict.Add(x.MessageID, x));
            return dict;
        }

        public void Store(Dictionary<long, BankMessage.BankMessage> messages)
        {
            var messagesList = messages.Values.ToList();

            var serializer = new XmlSerializer(typeof(List<BankMessage.BankMessage>));

            using (TextWriter tw = new StreamWriter(fileName, false))
            {
                serializer.Serialize(tw, messagesList);
            }
        }
    }
}