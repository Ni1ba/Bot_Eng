using Newtonsoft.Json;

namespace Bot_Eng
{
    public class BOT
    {
        private string _JsonTxt;
        public Dictionary<string, string> ConfigJson;

        public void Start()
        {
            InitConfigJson();

        }

        //файл конфигурации определить
        private string ReadJsonConfig()
        {
            string jsonTXT = System.IO.File.ReadAllText("");// путь к файлу конфига
            return jsonTXT;
        }

        private void InitConfigJson()
        {
            _JsonTxt = ReadJsonConfig();
            ConfigJson = JsonConvert.DeserializeObject<Dictionary<string, string>>(_JsonTxt);
        }
    }
}
