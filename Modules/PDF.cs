using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;


namespace MycGroupApp
{
    static class PDF
    {
        static public void encabezadoPDF(List<string> encabezados, ref PdfPTable table, ref Document doc)
        {

            iTextSharp.text.Font StandarFont = new iTextSharp.text.Font(
                iTextSharp.text.Font.FontFamily.HELVETICA,
                8,
                iTextSharp.text.Font.NORMAL,
                BaseColor.BLACK
                );


            foreach (string x in encabezados)
            {

                PdfPCell cell = new PdfPCell(new Phrase(x, StandarFont));
                cell.BorderWidth = 0;
                cell.BorderWidthBottom = 0.75f;
                table.AddCell(cell);
            }
            doc.Add(table);
        }
        static public void filaPDF(DataGridViewRow x, ref Document doc)
        {

            iTextSharp.text.Font StandarFont = new iTextSharp.text.Font(
                iTextSharp.text.Font.FontFamily.HELVETICA,
                8,
                iTextSharp.text.Font.NORMAL,
                BaseColor.BLACK
                );
            PdfPTable table = new PdfPTable(x.Cells.Count);
            table.WidthPercentage = 100;

            PdfPCell cell;
            for (int j = 0; j < x.Cells.Count; j++)
            {

                cell = new PdfPCell(new Phrase(x.Cells[j].Value.ToString(), StandarFont));
                cell.BorderWidth = 0;
                cell.BorderWidthTop = 0.75f;
                table.AddCell(cell);
            }



            doc.Add(table);


        }
      
        static public void listadoDe(DataGridView x,string titulo,string descripcion)
        {

            if ((x.RowCount > 0)&&(comprobarDireccion()))
            {
                string archivo = $"{titulo}-{DateTime.Now.ToString("dd-MM-yyyy")}.pdf";
                FileStream p = new FileStream(Router.Archivos +archivo, FileMode.Create);
                Document doc = new Document(PageSize.LETTER, 5, 5, 7, 7);
                PdfWriter pw = PdfWriter.GetInstance(doc, p);
                doc.Open();
                //titulo y autor
                doc.AddTitle(titulo);
                doc.AddAuthor("Destored");
                // define tipo de fuente (tipo,tamaño,forma,color)

                iTextSharp.text.Font StandarFont = new iTextSharp.text.Font(
                    iTextSharp.text.Font.FontFamily.HELVETICA,
                    8,
                    iTextSharp.text.Font.NORMAL,
                    BaseColor.BLACK
                    );

                //escribir encabezado
               
                doc.Add(new Paragraph(titulo));
                doc.Add(Chunk.NEWLINE);
                doc.Add(new Phrase("Fecha emisión: " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss"), StandarFont));
                doc.Add(Chunk.NEWLINE);
                doc.Add(new Phrase("Descripción: " + descripcion));
                doc.Add(Chunk.NEWLINE);

                List<string> encabezado = new List<string>();
                for (int i = 0; i < x.ColumnCount; i++)
                {
                    encabezado.Add(x.Columns[i].HeaderText);
                }
                PdfPTable table = new PdfPTable(x.ColumnCount);
                table.WidthPercentage = 100;
                encabezadoPDF(encabezado, ref table, ref doc);
                for (int i = 0; i < x.RowCount; i++)
                {

                    filaPDF(x.Rows[i], ref doc);
                    doc.Add(Chunk.NEWLINE);
                }

                //agregando datos

                doc.Close();
                pw.Close();

                MessageBox.Show("Documento generado existosamente", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                try
                {

                    var ph = new Process();
                    ph.StartInfo = new ProcessStartInfo(Router.Archivos + archivo)
                    {
                        UseShellExecute = true
                    };
                    ph.Start();
                }
                catch (Exception err) { MessageBox.Show(err.Message); }
            }
            else
            {
                if(x.RowCount == 0) { MessageBox.Show("Lista vacía"); }
                else { MessageBox.Show("Error en la ubicación del archivo"); }
                
            }
       
        
        }

        static public void RemitoServicio()
        {

                string dirImagen = @"C:\\Users\\Luciano\\OneDrive\\DesTored\\Producción\\AseguradoraApp\\Project - Sistema Gestion\\Contido Digital\\logo_destored.png";
                string archivo = $"Remito.pdf";
                FileStream p = new FileStream(Router.Archivos + archivo, FileMode.Create);
                Document doc = new Document(PageSize.LETTER.Rotate(), 5, 5, 7, 7);
                PdfWriter pw = PdfWriter.GetInstance(doc, p);
                doc.Open();
                //titulo y autor
                doc.AddTitle("Remito Servicio");
                Image imagen = Image.GetInstance(dirImagen);
                imagen.ScaleToFit(200f, 200f); // Ajusta el tamaño de la imagen
                float imagenWidth = imagen.ScaledWidth;
                float imagenHeight = imagen.ScaledHeight;

                float pageWidth = PageSize.LETTER.Width;
                float pageHeight = PageSize.LETTER.Height;

                float positionX = (pageWidth - imagenWidth) / 2;
                float positionY = pageHeight - imagenHeight;
                imagen.SetAbsolutePosition(positionX, positionY);                  doc.Add(imagen);


            doc.AddAuthor("Destored");
                // define tipo de fuente (tipo,tamaño,forma,color)

                iTextSharp.text.Font StandarFont = new iTextSharp.text.Font(
                    iTextSharp.text.Font.FontFamily.COURIER,
                    12,
                    iTextSharp.text.Font.NORMAL,
                    BaseColor.BLACK
                    );

                //escribir encabezado

                doc.Add(new Paragraph(""));
                doc.Add(Chunk.NEWLINE);
                doc.Add(new Phrase("Fecha impresión: " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss"), StandarFont));
                doc.Add(Chunk.NEWLINE);
            /*
                List<string> encabezado = new List<string>();
                for (int i = 0; i < 5; i++)
                {
                    encabezado.Add("");
                }
                PdfPTable table = new PdfPTable(5);
                table.WidthPercentage = 100;
                encabezadoPDF(encabezado, ref table, ref doc);
                 filaPDF(x.Rows[i], ref doc);
                doc.Add(Chunk.NEWLINE);
            */
            //agregando datos

            doc.Close();
                pw.Close();

                MessageBox.Show("Documento generado existosamente", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                try
                {

                    var ph = new Process();
                    ph.StartInfo = new ProcessStartInfo(Router.Archivos + archivo)
                    {
                        UseShellExecute = true
                    };
                    ph.Start();
                }
                catch (Exception err) { MessageBox.Show(err.Message); }
           


        }

        static public bool comprobarDireccion()
        {
            string user = Environment.UserName;
            
            int i = 0;
            if (Directory.Exists(Router.Archivos))
            {
                return true;
            }
            else
            {
                try
                {
                     Directory.CreateDirectory(Router.Archivos);
                     return true;
                }
                catch (Exception err) {
                    MessageBox.Show(err.Message);
                
                }
            }
            return false;
        }


        static public bool CrearRemitoOperacion(string nombreArchivo, bool band)
        {
            // Ruta de salida del archivo PDF
            string outputPath = Router.Archivos+nombreArchivo;

            // Crear el documento PDF
            Document doc = new Document(PageSize.A4.Rotate(), 5, 5, 7, 7);

            // Especificar la ruta del archivo de salida
            PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(outputPath, FileMode.Create));

            doc.Open();

            // Cargar la imagen desde un archivo
            string imagePath = Router.Captura; // Ruta de la imagen
            iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(imagePath);

            // Ajustar el tamaño de la imagen si es necesario
            img.ScaleToFit(PageSize.A4.Height, PageSize.A4.Width);

            // Agregar la imagen al documento PDF
            doc.Add(img);

            doc.Close();
            writer.Close();
           
            if (band)
            {
                string folderPath = Router.Archivos; // Ruta de la carpeta que deseas abrir
                try
                {
                    Process.Start(folderPath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al abrir la carpeta: " + ex.Message);
                }

            }
            else
            {
                

                try
                {
                    Process.Start(outputPath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al abrir el archivo PDF: " + ex.Message);
                }

            }
           
            return true;
        }

       

    }
}
