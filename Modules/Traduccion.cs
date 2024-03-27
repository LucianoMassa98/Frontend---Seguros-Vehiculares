using Google.Cloud.Translation.V2;
using System;

namespace MycGroupApp
{
    static class Traduccion
    {
        static public string traducir(string sourceText)
        {

            /*TranslationClient client = TranslationClient.Create();

            string targetLanguage = "es"; // Código de idioma para español

            // Realizar la traducción
            string translatedText = client.TranslateText(sourceText, targetLanguage).TranslatedText;
            */
            return sourceText;
        }
    }

    
}
