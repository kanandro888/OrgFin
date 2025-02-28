using OfficeOpenXml;
using System;
using System.IO;

namespace SAPIExcel.Controllers
{
    public class Metodos
    {
        string caminhoArquivo = Path.Combine("/tmp", "projFin_prototipo_app.xlsx");

        public void SalvarNoExcel(DateTime data, int mes, int ano, decimal valor, string descricao, string categoria)
        {
            try
            {
                if (!File.Exists(caminhoArquivo))
                    throw new Exception("Arquivo Excel não encontrado.");

                // Cria ou abre o arquivo Excel
                FileInfo fileInfo = new FileInfo(caminhoArquivo);
                using (var package = new ExcelPackage(fileInfo))
                {
                    var workbook = package.Workbook;
                    var worksheet = ObterAba(workbook, "saidas");

                    // Encontra a próxima linha disponível
                    var ultimaLinha = worksheet.Dimension?.End.Row ?? 0;
                    int linha = ultimaLinha + 1;

                    // Insere os dados nas células
                    worksheet.Cells[linha, 1].Value = data.ToString("dd/MM/yyyy");
                    worksheet.Cells[linha, 2].Value = mes;
                    worksheet.Cells[linha, 3].Value = ano;
                    worksheet.Cells[linha, 4].Value = valor;
                    worksheet.Cells[linha, 5].Value = descricao;
                    worksheet.Cells[linha, 6].Value = categoria;

                    // Salva as mudanças
                    package.Save();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao salvar no Excel: {ex.Message}");
            }
        }

        // Método para obter a aba do Excel
        private ExcelWorksheet ObterAba(ExcelWorkbook workbook, string aba)
        {
            var worksheet = workbook.Worksheets[aba];
            if (worksheet == null)
            {
                throw new Exception($"A aba '{aba}' não foi encontrada.");
            }
            return worksheet;
        }
    }
}
