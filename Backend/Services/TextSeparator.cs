using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ServiceWeb.Services
{
    public class TextSeparator
    {
        public async Task<string[]> SeparateTextAsync(string inputText)
        {
            return await Task.Run(() =>
            {
                // Regex para remover o número seguido de ponto e espaço no início de cada linha
                string pattern = @"^\d+\.\s*";
                string result = Regex.Replace(inputText, pattern, "", RegexOptions.Multiline);
                string[] lines = result.Split(new[] { '\n' }, StringSplitOptions.TrimEntries);

                return lines;
            });
        }
    }
}
