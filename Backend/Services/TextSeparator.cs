using System;
using System.Threading.Tasks;

namespace ServiceWeb.Services
{
    public class TextSeparator
    {
        public async Task<string[]> SeparateTextAsync(string inputText)
        {
            return await Task.Run(() =>
            {
                string input = inputText;

                // Separar o texto em linhas
                string[] lines = input.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

                // Array fixo de tamanho 9
                string[] musicArray = new string[9];

                // Preencher o array com as músicas
                for (int i = 0; i < lines.Length && i < musicArray.Length; i++)
                {
                    // Remover os números e espaços extras no início de cada linha
                    musicArray[i] = lines[i].Substring(lines[i].IndexOf(".") + 2).Trim();
                }

                return musicArray;
            });
        }
    }
}
