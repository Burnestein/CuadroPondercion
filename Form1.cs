using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.IO.Image;
using System.Drawing.Drawing2D;

namespace CuadroPondercion
{
    public partial class Form1 : Form
    {
        private bool estado = false;
        private int social, semiSocial, servicio, privado;
        int tam;
        int rango;
        private int borderRadius = 20;
        private Color borderColor = Color.FromArgb(128, 128, 255);

        public Graphics ZonaDibujo;
        List<TextBox> listaTextBoxes;
        List<TextBox> listaTextBoxSuma;
        List<TextBox> listaTextBoxRango;

        List<CEspacio> listaEspacios;
        List<CAreaGrafico> listaAreas;
        List<string> areasEspacios;
        List<string> nombresEspacios;
        List<int> rangos;
        List<int> numeros;
        DataTable dt;

        //Selecciona la posicion del primer rombo

        int Si = 0;
        int Ti = 13;
        int Ui = -30;
        int Vi = 33;
        int Wi = 0;
        int Xi = 53;
        int Yi = 30;
        int Zi = 33;

        int rowIndex = 0;
        public Form1()
        {
            InitializeComponent();
            ZonaDibujo = panel2.CreateGraphics();
            tam = 10;
            social = 0;
            semiSocial = 0;
            servicio = 0;
            privado = 0;
            rango = 1;

            listaTextBoxes = new List<TextBox>();
            listaTextBoxSuma = new List<TextBox>();
            listaTextBoxRango = new List<TextBox>();
            listaEspacios = new List<CEspacio>();
            listaAreas = new List<CAreaGrafico>();
            nombresEspacios = new List<string>();
            areasEspacios = new List<string>();
            rangos = new List<int>();
            numeros = new List<int>();
            dt = new DataTable();
            dt.Columns.Add("Area");
            dt.Columns.Add("Espacio");
            dataGridView1.DataSource = dt;
            comboBox1.SelectedIndex = 1;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.Single;
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.Single;
            dataGridView1.GridColor = Color.Black;
            //dataGridView1.AutoGenerateColumns = false; // Desactiva la generación automática de columnas
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                row.Height = 40; // Establece la altura de todas las filas a 50 píxeles

            }
            dataGridView1.ClearSelection();
            rowIndex = -1;
        }

        private void GenerarTxtBox()
        {
            BorrarTxtBox();
            int N = 20;
            int M = 45;

            for (int j = 0; j < tam; j++)
            {

                int n = N;
                int m = M;

                for (int q = tam; q >= j; q--)
                {

                    //Crea las filas de TextBox
                    TextBox text = new TextBox();
                    text.Location = new Point(n, m);
                    text.TextAlign = HorizontalAlignment.Center;
                    text.MaxLength = 1;
                    text.MaximumSize = new Size(20, 18);
                    text.Text = "0";
                    panel2.Controls.Add(text);
                    listaTextBoxes.Add(text); // Agrega el TextBox a la lista

                    n += 30;
                    m += 20;

                }
                M += 40;

            }
            TxTBoxResultados();
        }

        private void BorrarTxtBox()
        {
            foreach (TextBox textBox in listaTextBoxes)
            {
                panel2.Controls.Remove(textBox); // Elimina el TextBox del control del panel
                textBox.Dispose(); // Libera recursos asociados al TextBox
            }
            listaTextBoxes.Clear(); // Limpia la lista
            foreach (TextBox textBox in listaTextBoxSuma)
            {
                panel2.Controls.Remove(textBox); // Elimina el TextBox del control del panel
                textBox.Dispose(); // Libera recursos asociados al TextBox
            }
            listaTextBoxSuma.Clear();
            foreach (TextBox textBox in listaTextBoxRango)
            {
                panel2.Controls.Remove(textBox); // Elimina el TextBox del control del panel
                textBox.Dispose(); // Libera recursos asociados al TextBox
            }
            listaTextBoxRango.Clear();
        }
        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            tam = listaEspacios.Count; // IMPORTANTE de este tamaño sera la matriz
            Console.WriteLine("tam = " + tam);
            ZonaDibujo.Clear(Color.White);
            CRombo MiRombo;


            int S = Si;
            int T = Ti;
            int U = Ui;
            int V = Vi;
            int W = Wi;
            int X = Xi;
            int Y = Yi;
            int Z = Zi;

            for (int i = 0; i < tam; i++)
            {
                //Crea las columnas de rombos
                MiRombo = new CRombo(S, T, U, V, W, X, Y, Z, Color.Black);
                MiRombo.Dibuja(ZonaDibujo);

                int s = S;
                int t = T;
                int u = U;
                int v = V;
                int w = W;
                int x = X;
                int y = Y;
                int z = Z;

                for (int n = tam + 2; n > i; n--)
                {/*Tener siempre 2+ que el For anterior*/
                    //Crea las filas de rombos
                    MiRombo = new CRombo(s, t, u, v, w, x, y, z, Color.Black);
                    MiRombo.Dibuja(ZonaDibujo);

                    s += 30;
                    t += 20;
                    u += 30;
                    v += 20;
                    w += 30;
                    x += 20;
                    y += 30;
                    z += 20;
                }

                T += 40;
                V += 40;
                X += 40;
                Z += 40;
            }


        }


        private void dibujarMatriz()
        {
            tam = listaEspacios.Count; // IMPORTANTE de este tamaño sera la matriz
            Console.WriteLine("tam = "+tam);
            ZonaDibujo.Clear(Color.White);
            CRombo MiRombo;


            int S = Si;
            int T = Ti;
            int U = Ui;
            int V = Vi;
            int W = Wi;
            int X = Xi;
            int Y = Yi;
            int Z = Zi;

            for (int i = 0; i < tam; i++)
            {
                //Crea las columnas de rombos
                MiRombo = new CRombo(S, T, U, V, W, X, Y, Z, Color.Black);
                MiRombo.Dibuja(ZonaDibujo);

                int s = S;
                int t = T;
                int u = U;
                int v = V;
                int w = W;
                int x = X;
                int y = Y;
                int z = Z;

                for (int n = tam + 2; n > i; n--)
                {/*Tener siempre 2+ que el For anterior*/
                    //Crea las filas de rombos
                    MiRombo = new CRombo(s, t, u, v, w, x, y, z, Color.Black);
                    MiRombo.Dibuja(ZonaDibujo);

                    s += 30;
                    t += 20;
                    u += 30;
                    v += 20;
                    w += 30;
                    x += 20;
                    y += 30;
                    z += 20;
                }

                T += 40;
                V += 40;
                X += 40;
                Z += 40;
            }

            GenerarTxtBox();
        }

        private void TxTBoxResultados() // determina cuales son os txtbox de Sumatorias de la tabla
        {
            int cant = listaTextBoxes.Count();
            int t = tam;
            int col = 0;
            List<int> iBorrar = new List<int>(); // lista temporal que guarda los indices que se van a remover de la lista general
            for(int i = 0; i < cant; i++)
            {
                
                if(col == t - 1) // separa los txtbox de Sumatoria de los demás
                {
                    listaTextBoxes[i].BackColor = Color.White; // cambio color de fondo
                    listaTextBoxes[i].BorderStyle = BorderStyle.None;
                    listaTextBoxes[i].ReadOnly = true;  // bloquea la escritura
                    listaTextBoxSuma.Add(listaTextBoxes[i]); // se agrega a una tabla aparte
                    iBorrar.Add(i); // se guarda el indice para removerlo de listaTextBox mas adelante
                }
                if(col == t) // separa los txtbox de Rango de los demás
                {
                    listaTextBoxes[i].BackColor = Color.White;
                    listaTextBoxes[i].BorderStyle = BorderStyle.None;
                    listaTextBoxRango.Add(listaTextBoxes[i]);
                    listaTextBoxes[i].ReadOnly = true;  // bloquea la escritura
                    iBorrar.Add(i);

                    t -= 1;
                    col = -1;
                }
                col++;
            }

            /*Console.WriteLine("Los indices a borrar son:\n");
            for(int i = 0; i < iBorrar.Count; i++)
            {
                Console.WriteLine(iBorrar[i] + "\n");
            }*/
            int contador = iBorrar.Count-1;
            for(int i=contador; i>=0; i--) // Borra los elementos de listaTextBoxes en los indices de iBorrar, IMPORTANTE: se deben eliminar en orden inverso
            {
                listaTextBoxes.RemoveAt(iBorrar[i]);

            }

            //Comprobar que se separaron los txtBox de los txtBox de resultados
            for(int i=0; i < listaTextBoxes.Count; i++)
            {
                listaTextBoxes[i].BackColor = Color.White;
            }
            
        }

        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                row.Height = 40; // Establece la altura de todas las filas a 50 píxeles
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Verifica si se hizo clic en una celda válida
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                // Obtiene el índice de la fila seleccionada
                rowIndex = e.RowIndex;
                Console.WriteLine("El indice de la fila es: " + rowIndex);

                
            }
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                if (dataGridView1.Columns[e.ColumnIndex].Name == "Area" || dataGridView1.Columns[e.ColumnIndex].Name == "Espacio")
                {
                    string areaValue = dataGridView1.Rows[e.RowIndex].Cells["Area"].Value as string;

                    if (areaValue != null)
                    {
                        Color rowColor = GetRowColor(areaValue);
                        dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = rowColor;
                    }
                }
            }
        }

        private Color GetRowColor(string area)
        {
            switch (area)
            {
                case "Social":
                    return Color.Green;
                case "Semi-Social":
                    return Color.Orange;
                case "Servicio":
                    return Color.Yellow;
                case "Privada":
                    return Color.Red;
                default:
                    return dataGridView1.DefaultCellStyle.BackColor; // Color por defecto
            }
        }

        private void button3_Click(object sender, EventArgs e) // Hace las sumatorias y saca los Rangos
        {
            
            numeros = ConvertirTextBoxAEnteros(listaTextBoxSuma); // obtengo una lista de los numeros de sumatoria
            Console.WriteLine("Números originales: " + string.Join(", ", numeros));

            rangos = generarRangos(numeros); // obtengo una lista de rangos sincronizada con el orden de la tabla
            Console.WriteLine("Rangos asignados: " + string.Join(", ", rangos));

            Console.WriteLine("Convertimos la lista de rangos a listaTextBoxRango");
            ConvertirEnterosATextBox(rangos);
            Console.WriteLine("Los valores de rango han pasado a listaTextBoxRango");

            

            //agregar los rangos a la lista de espacios


            panel3.Invalidate();
        }

        private void generarSumas()
        {
            int cant = listaTextBoxes.Count();
            int contador = 0;
            int suma = 0;
            int t = tam;

            for (int col = 0; col < 2; col++)
            {

                if (col == 0) // suma la primer columna de derecha a izquierda, de arriba a abajo.
                {
                    for (int j = 0; j < t - 1; j++) // t es el tamaño o altura de la columna
                    {
                        suma += int.Parse(listaTextBoxes[contador].Text); //suma cada elemento de las filas de la columna
                        Console.WriteLine("Suma primer columna: " + suma);
                        contador++;
                    }
                    Console.WriteLine("Contador queda en: " + contador);
                    listaTextBoxSuma[col].Text = suma.ToString();
                    t--; //reduce el tamaño de la columna
                    suma = 0;

                }
                else if (col > 0)
                {
                    int t1 = 0;
                    int t2 = tam - 1;
                    int index;
                    int temp = contador;
                    int sumatemp = suma;
                    for (int xpos = 1; xpos < listaTextBoxSuma.Count(); xpos++)
                    {
                        if (xpos != listaTextBoxSuma.Count() - 1)
                        {
                            for (int j = 0; j < t - 1; j++) //suma las columnas
                            {
                                Console.WriteLine("suma " + int.Parse(listaTextBoxes[contador].Text));
                                suma += int.Parse(listaTextBoxes[contador].Text);
                                Console.WriteLine("Suma columna: " + suma);
                                listaTextBoxSuma[xpos].Text = suma.ToString();
                                contador++;

                            }
                        }

                        Console.WriteLine("Contador queda en: " + contador);
                        //le suma las filas
                        Console.WriteLine("Inicia suma de filas");




                        Console.WriteLine("Suma anterior: " + suma);
                        Console.WriteLine("Col = " + col);
                        Console.WriteLine("listaTextBoxSuma.Count = " + listaTextBoxSuma.Count());
                        Console.WriteLine("t1 = " + t1);
                        Console.WriteLine("t2 = " + t2);
                        Console.WriteLine("cant = " + cant);
                        Console.WriteLine("xpos = " + xpos);
                        for (int i = xpos - 1; i >= 0; i--)
                        {
                            //if (i != xpos-1) suma = 0;
                            index = cant - (cant - (i)) + t1;
                            Console.WriteLine("index = " + index);
                            t1 += t2;
                            Console.WriteLine("t1 += t2 -> " + t1);
                            t2--;
                            Console.WriteLine("t2 = " + t2);
                            Console.WriteLine("suma += " + listaTextBoxes[index].Text);

                            suma += int.Parse(listaTextBoxes[index].Text);

                            listaTextBoxSuma[xpos].Text = suma.ToString();
                            Console.WriteLine("suma total = " + suma);

                        }
                        Console.WriteLine("Termina suma de filas en esta columna");
                        listaTextBoxSuma[xpos].Text = suma.ToString();
                        t--;
                        suma = 0;
                        t1 = 0;
                        t2 = tam - 1;

                    }
                    Console.WriteLine("Termina suma de filas en todas las columnas");

                }
                if (col != 0)
                {
                    contador = 0;
                    suma = 0;
                    t = tam;
                }

            }
        }

        private List<int> generarRangos(List<int> numeros) // genera una lista de los rangos y los asigna en la tabla
        {
            List<int> rangos = new List<int>();
            Dictionary<int, int> rangoPorNumero = new Dictionary<int, int>();

            // Clonamos la lista de números y la ordenamos en orden descendente
            List<int> numerosOrdenados = new List<int>(numeros);
            numerosOrdenados.Sort((a, b) => b.CompareTo(a));

            int rangoActual = 1;

            foreach (int numero in numerosOrdenados)
            {
                if (!rangoPorNumero.ContainsKey(numero))
                {
                    rangoPorNumero[numero] = rangoActual;
                    rango = rangoActual;
                    rangoActual++;
                }
            }

            // Ahora, asignamos los rangos en el orden original de la lista de números
            foreach (int numero in numeros)
            {
                rangos.Add(rangoPorNumero[numero]);
            }

            return rangos;
        }

        public List<int> ConvertirTextBoxAEnteros(List<TextBox> listaTextBox)
        {
            List<int> listaEnteros = new List<int>();

            foreach (TextBox textBox in listaTextBox)
            {
                string valor = textBox.Text;

                if (int.TryParse(valor, out int entero))
                {
                    listaEnteros.Add(entero);
                }
                else
                {
                    // Manejo de errores si la conversión falla.
                    Console.WriteLine($"No se pudo convertir el valor '{valor}' en un entero.");
                }
            }

            return listaEnteros;
        }

        public void ConvertirEnterosATextBox(List<int> listaEnteros)
        {
            for(int i=0; i<listaTextBoxRango.Count; i++)
            {
                listaTextBoxRango[i].Text = listaEnteros[i].ToString(); // Convierte el entero a una cadena
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }
        private Bitmap CaptureControl(Control control)
        {
            Bitmap bitmap = new Bitmap(control.Width, control.Height);
            control.DrawToBitmap(bitmap, new Rectangle(0, 0, control.Width, control.Height));
            return bitmap;
        }

        //Método para exportar imagen del diseño creado.
        private void comoJPGToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Crear el objeto SaveFileDialog
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            // Establecer filtros y opciones del cuadro de diálogo
            saveFileDialog.Filter = "Archivos PNG(*.pdf)|*.png|Todos los archivos (*.*)|*.*";
            saveFileDialog.Title = "Guardar como";
            saveFileDialog.DefaultExt = "png";

            // Mostrar el cuadro de diálogo
            DialogResult result = saveFileDialog.ShowDialog();

            // Verificar si el usuario hizo clic en "Guardar"
            if (result == DialogResult.OK)
            {
                // Obtener la ruta completa del archivo seleccionado
                string rutaArchivo = saveFileDialog.FileName;

                // Llamar al método para guardar el panel y la imagen en un archivo PDF
                GuardarComoJPG(rutaArchivo);

                // Mostrar un mensaje de éxito
                MessageBox.Show("Imagen guardada exitosamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void GuardarComoJPG(string ruta)
        {
            // Capturar la imagen de la tabla
            Bitmap tablaBitmap = CaptureControl(dataGridView1);

            // Capturar la imagen del panel
            Bitmap panelBitmap = CaptureControl(panel2);

            // Crear una nueva imagen combinando la tabla y el panel
            int nuevaAnchura = tablaBitmap.Width + panelBitmap.Width;
            int nuevaAltura = Math.Max(tablaBitmap.Height, panelBitmap.Height);

            Bitmap imagenCombinada = new Bitmap(nuevaAnchura, nuevaAltura);

            using (Graphics g = Graphics.FromImage(imagenCombinada))
            {
                g.DrawImage(tablaBitmap, 0, 0);
                g.DrawImage(panelBitmap, tablaBitmap.Width, 0);
            }

            // Guardar la imagen combinada
            imagenCombinada.Save(ruta, System.Drawing.Imaging.ImageFormat.Png);

            // Abrir la imagen guardada
            if (File.Exists(ruta))
            {
                MessageBox.Show("Imagen guardada exitosamente.");
                System.Diagnostics.Process.Start(ruta);
            }
            else
            {
                MessageBox.Show("Error al guardar la imagen.");
            }
        }

        //Metodo para indicar donde guardar el archivo PDF y generarlo.
        private void comoPDFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Crear el objeto SaveFileDialog
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            // Establecer filtros y opciones del cuadro de diálogo
            saveFileDialog.Filter = "Archivos PDF(*.pdf)|*.png|Todos los archivos (*.*)|*.*";
            saveFileDialog.Title = "Guardar como";
            saveFileDialog.DefaultExt = "pdf";

            // Mostrar el cuadro de diálogo
            DialogResult result = saveFileDialog.ShowDialog();

            // Verificar si el usuario hizo clic en "Guardar"
            if (result == DialogResult.OK)
            {
                // Obtener la ruta completa del archivo seleccionado
                string rutaArchivo = saveFileDialog.FileName;

                // Llamar al método para guardar el panel y la imagen en un archivo PDF
                GuardarComoPDF(rutaArchivo);

                // Mostrar un mensaje de éxito
                MessageBox.Show("Archivo PDF guardado exitosamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //Codigo para exportar el archivo a PDF //No esta finalizado. marca error al guardar 
        private void GuardarComoPDF(string pdfPath)
        {
            // Capturar la imagen de la tabla
            Bitmap tablaBitmap = CaptureControl(dataGridView1);

            // Capturar la imagen del panel
            Bitmap panelBitmap = CaptureControl(panel2);

            // Crear una nueva imagen combinando la tabla y el panel
            int nuevaAnchura = tablaBitmap.Width + panelBitmap.Width;
            int nuevaAltura = Math.Max(tablaBitmap.Height, panelBitmap.Height);

            Bitmap imagenCombinada = new Bitmap(nuevaAnchura, nuevaAltura);

            using (Graphics g = Graphics.FromImage(imagenCombinada))
            {
                g.DrawImage(tablaBitmap, 0, 0);
                g.DrawImage(panelBitmap, tablaBitmap.Width, 0);
            }
            //Hasta aqui creamos y combinamos el bitmap del datagridview y el panel.
            using (var writer = new PdfWriter(pdfPath))
            {
                using (var pdf = new PdfDocument(writer))
                {
                    Document document = new Document(pdf, iText.Kernel.Geom.PageSize.A4);

                    // Convertir el Bitmap a byte[]
                    byte[] bitmapData = ImageToBytes(imagenCombinada);

                    // Crear un objeto Image de iTextSharp
                    iText.Layout.Element.Image image = new iText.Layout.Element.Image(ImageDataFactory.Create(bitmapData));

                    // Añadir la imagen al documento PDF
                    document.Add(image);

                    Console.WriteLine("PDF creado exitosamente con la imagen.");
                }
            }


            // Abrir la imagen guardada
            if (File.Exists(pdfPath))
            {
                MessageBox.Show("Imagen guardada exitosamente.");
                System.Diagnostics.Process.Start(pdfPath);
            }
            else
            {
                MessageBox.Show("Error al guardar la imagen.");
            }
        }

        // Convertir un objeto Bitmap a byte[]
        static byte[] ImageToBytes(Bitmap bitmap)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                return stream.ToArray();
            }
        }


        private void dataGridView1_KeyDown(object sender, KeyEventArgs e) // 
        {
            if (e.KeyCode == Keys.Escape)
            {
                Console.WriteLine("Quitar seleccion");
                dataGridView1.ClearSelection(); // Quita la seleccion del datagridview
                rowIndex = -1;
            }
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

            listaAreas.Clear();
            int totalAreas = social + semiSocial + servicio + privado;

            int width = 400;
            int height = 400;
            int centerX = width / 2;
            int centerY = height / 2;

            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            
            if (estado)
            {
                int circleCount = Math.Min(10, rango);

                int circleRadius = 30;

                for (int i = 0; i < circleCount; i++)
                {
                    int radius = circleRadius * (i + 1);

                    double total = social + semiSocial + servicio + privado;
                    double angle = 360.0 / total;

                    DrawColoredCircle(g, centerX, centerY, radius, social, semiSocial, servicio, privado, angle);
                }

                for (int i = 0; i < circleCount; i++)
                {
                    int radius = circleRadius * (i + 1);
                    g.DrawEllipse(Pens.Black, centerX - radius, centerY - radius, 2 * radius, 2 * radius);
                }

                for (int i = 0; i < circleCount; i++)
                {
                    int radius = circleRadius * (i + 1);
                    //DrawCircleNumber(g, i + 1, centerX, centerY, radius); // Dibujar el número del círculo
                }

                //
                Font font = new Font("Arial", 8, FontStyle.Bold);
                SolidBrush brush = new SolidBrush(Color.White);
                
                if (rangos.Count > 0)
                {
                    for (int rangoActual = 1; rangoActual <= circleCount; rangoActual++)
                    {
                        List<string> nombresEnRango = dataGridView1.Rows.Cast<DataGridViewRow>().Select(row => row.Cells["Espacio"].Value.ToString()).ToList(); // Obtener los nombres de espacios para el rango actual
                        Console.WriteLine("Espacios:");
                        Console.WriteLine(string.Join(", ", nombresEnRango));
                        List<string> areasEnRango = dataGridView1.Rows.Cast<DataGridViewRow>().Select(row => row.Cells["Area"].Value.ToString()).ToList(); // Obtener los nombres de areas para el rango actual
                        Console.WriteLine("Areas de Espacios:");
                        Console.WriteLine(string.Join(", ", areasEnRango));
                        for (int i=0; i < nombresEnRango.Count; i++) Console.WriteLine("Nombre y area: " + nombresEnRango[i] + " - " + areasEnRango[i]);

                        if (nombresEnRango.Any())
                        {
                            Console.WriteLine("Entra al If(nombresEnRango.Any())");
                            int radius = circleRadius * rangoActual; //aqui decide el radio
                            Console.WriteLine("listaAreas.Count: "+ listaAreas.Count);

                            Console.WriteLine("Entra al If(listaAreas.Any())");
                            // Establecer el formato para el texto (alineación, etc.)
                            StringFormat format = new StringFormat();
                            format.LineAlignment = StringAlignment.Center;
                            format.Alignment = StringAlignment.Center;
                            double angle;
                            double midAngle;
                            double x;
                            double y;
                            int cantidadSocial = areasEnRango.Count(area => area == "Social");
                            int cantidadSemi = areasEnRango.Count(area => area == "Semi-Social");
                            int cantidadServicio = areasEnRango.Count(area => area == "Servicio");
                            int cantidadPrivada = areasEnRango.Count(area => area == "Privada");
                            int contSocial = 0;
                            int contSemi = 0;
                            int contServ = 0;
                            int contPriv = 0;
                            int sub = 4;
                            for (int j = 0; j < nombresEnRango.Count; j++) // 4 es la cantidad de tipos de areas que hay
                            {
                                switch (areasEnRango[j])
                                {
                                    case "Social":
                                        contSocial++;
                                        Console.WriteLine("Hay un elemento en Social");
                                        //listaAreas[0].espacios.Add(nombresEnRango[j]);
                                        //listaAreas[0].rangos.Add(rangos[j]);

                                        angle = listaAreas[0].anguloInicio;
                                        midAngle = listaAreas[0].anguloFin / cantidadSocial;
                                        x = centerX + radius * Math.Cos((angle + (midAngle * contSocial) - (midAngle / 2)) * Math.PI / 180); // Coordenada X del círculo del nombre del espacio
                                        y = centerY + radius * Math.Sin((angle + (midAngle * contSocial) - (midAngle / 2)) * Math.PI / 180); // Coordenada Y del círculo del nombre del espacio

                                        // Dibujar el nombre del espacio con el formato determinado
                                        if (rangos[j] == rangoActual)
                                        {
                                            SolidBrush circleBrush = new SolidBrush(Color.Black); // Cambia Color.Red por el color que desees
                                            Pen circlePen = new Pen(Color.White, 1);
                                            int circleSize = 30;
                                            // Dibujar un círculo detrás del texto
                                            g.FillEllipse(circleBrush, (float)x - circleSize/2, (float)y - circleSize/2, circleSize, circleSize);
                                            g.DrawEllipse(circlePen, (float)x - circleSize/2, (float)y - circleSize/2, circleSize, circleSize);
                                            
                                            if (nombresEnRango[j].Length < sub) sub = nombresEnRango[j].Length;
                                            g.DrawString(nombresEnRango[j].Substring(0,sub), font, brush, new RectangleF((float)x - 30, (float)y - 30, 60, 60), format);
                                            Console.WriteLine("Se dibuja " + nombresEnRango[j]);

                                            circleBrush.Dispose();
                                            circlePen.Dispose();
                                        }

                                        break;
                                    case "Semi-Social":
                                        contSemi++;
                                        Console.WriteLine("Hay un elemento en Semi-social");
                                        //listaAreas[1].espacios.Add(nombresEnRango[j]);
                                        //listaAreas[1].rangos.Add(rangos[j]);

                                        angle = listaAreas[1].anguloInicio;
                                        midAngle = listaAreas[1].anguloFin / cantidadSemi;
                                        x = centerX + radius * Math.Cos((angle + (midAngle * contSemi) - (midAngle / 2)) * Math.PI / 180); // Coordenada X del círculo del nombre del espacio
                                        y = centerY + radius * Math.Sin((angle + (midAngle * contSemi) - (midAngle / 2)) * Math.PI / 180); // Coordenada Y del círculo del nombre del espacio

                                        // Dibujar el nombre del espacio con el formato determinado
                                        if (rangos[j] == rangoActual)
                                        {
                                            SolidBrush circleBrush = new SolidBrush(Color.Black); // Cambia Color.Red por el color que desees
                                            Pen circlePen = new Pen(Color.White, 1);
                                            int circleSize = 30;
                                            // Dibujar un círculo detrás del texto
                                            g.FillEllipse(circleBrush, (float)x - circleSize / 2, (float)y - circleSize / 2, circleSize, circleSize);
                                            g.DrawEllipse(circlePen, (float)x - circleSize / 2, (float)y - circleSize / 2, circleSize, circleSize);
                                            
                                            if (nombresEnRango[j].Length < sub) sub = nombresEnRango[j].Length;
                                            g.DrawString(nombresEnRango[j].Substring(0, sub), font, brush, new RectangleF((float)x - 30, (float)y - 30, 60, 60), format);
                                            Console.WriteLine("Se dibuja " + nombresEnRango[j]);

                                            circleBrush.Dispose();
                                            circlePen.Dispose();
                                        }

                                        break;
                                    case "Servicio":
                                        contServ++;
                                        Console.WriteLine("Hay un elemento en Servicio");
                                        //listaAreas[2].espacios.Add(nombresEnRango[j]);
                                        //listaAreas[2].rangos.Add(rangos[j]);

                                        angle = listaAreas[2].anguloInicio;
                                        midAngle = listaAreas[2].anguloFin / cantidadServicio;
                                        x = centerX + radius * Math.Cos((angle + (midAngle * contServ) - (midAngle / 2)) * Math.PI / 180); // Coordenada X del círculo del nombre del espacio
                                        y = centerY + radius * Math.Sin((angle + (midAngle * contServ) - (midAngle / 2)) * Math.PI / 180); // Coordenada Y del círculo del nombre del espacio

                                        // Dibujar el nombre del espacio con el formato determinado
                                        if (rangos[j] == rangoActual)
                                        {
                                            SolidBrush circleBrush = new SolidBrush(Color.Black); // Cambia Color.Red por el color que desees
                                            Pen circlePen = new Pen(Color.White, 1);
                                            int circleSize = 30;
                                            // Dibujar un círculo detrás del texto
                                            g.FillEllipse(circleBrush, (float)x - circleSize / 2, (float)y - circleSize / 2, circleSize, circleSize);
                                            g.DrawEllipse(circlePen, (float)x - circleSize / 2, (float)y - circleSize / 2, circleSize, circleSize);
                                            
                                            if (nombresEnRango[j].Length < sub) sub = nombresEnRango[j].Length;
                                            g.DrawString(nombresEnRango[j].Substring(0, sub), font, brush, new RectangleF((float)x - 30, (float)y - 30, 60, 60), format);
                                            Console.WriteLine("Se dibuja " + nombresEnRango[j]);

                                            circleBrush.Dispose();
                                            circlePen.Dispose();
                                        }

                                        break;
                                    case "Privada":
                                        contPriv++;
                                        Console.WriteLine("Hay un elemento en Privada");
                                        //listaAreas[3].espacios.Add(nombresEnRango[j]);
                                        //listaAreas[3].rangos.Add(rangos[j]);

                                        angle = listaAreas[3].anguloInicio;
                                        midAngle = listaAreas[3].anguloFin / cantidadPrivada;
                                        x = centerX + radius * Math.Cos((angle + (midAngle * contPriv) - (midAngle / 2)) * Math.PI / 180); // Coordenada X del círculo del nombre del espacio
                                        y = centerY + radius * Math.Sin((angle + (midAngle * contPriv) - (midAngle / 2)) * Math.PI / 180); // Coordenada Y del círculo del nombre del espacio

                                        // Dibujar el nombre del espacio con el formato determinado
                                        Console.WriteLine("listaAreas[0].tipoArea: " + listaAreas[0].tipoArea + " == 0 " + " && " + "rangos[rangoActual-1]: " + rangos[rangoActual - 1] + " == rangoActual: " + rangoActual);
                                        if (rangos[j] == rangoActual)
                                        {
                                            SolidBrush circleBrush = new SolidBrush(Color.Black); // Cambia Color.Red por el color que desees
                                            Pen circlePen = new Pen(Color.White, 1);
                                            int circleSize = 30;
                                            // Dibujar un círculo detrás del texto
                                            g.FillEllipse(circleBrush, (float)x - circleSize / 2, (float)y - circleSize / 2, circleSize, circleSize);
                                            g.DrawEllipse(circlePen, (float)x - circleSize / 2, (float)y - circleSize / 2, circleSize, circleSize);
                                            
                                            if (nombresEnRango[j].Length < sub) sub = nombresEnRango[j].Length;
                                            g.DrawString(nombresEnRango[j].Substring(0, sub), font, brush, new RectangleF((float)x - 30, (float)y - 30, 60, 60), format);
                                            Console.WriteLine("Se dibuja " + nombresEnRango[j]);

                                            circleBrush.Dispose();
                                            circlePen.Dispose();
                                        }

                                        break;
                                    default:
                                        Console.WriteLine("Error al leer dato");
                                        break;
                                }
                            }
                            
                        }
                    }
                }


            }
            //nombresEspacios.Clear();
            //areasEspacios.Clear();
            listaAreas.Clear();
        }

        private void DrawColoredCircle(Graphics g, int centerX, int centerY, int radius, int social, int semiSocial, int servicio, int privado, double angle)
        {
            Brush[] brushes = { Brushes.Green, Brushes.Orange, Brushes.Yellow, Brushes.Red };
            int[] areas = { social, semiSocial, servicio, privado };
            listaAreas.Clear();
            double startAngle = 0;
            Console.WriteLine("areas.Length: "+areas.Length);
            for (int i = 0; i < areas.Length; i++)
            {
                    
                    CAreaGrafico Miarea = new CAreaGrafico(startAngle, i);
                    double sweepAngle = angle * areas[i];
                    Miarea.anguloFin = sweepAngle;
                    listaAreas.Add(Miarea); //Guarda el area del circulo creada
                    Console.WriteLine("Se agrega a listaAreas: "+ Miarea.tipoArea);
                    g.FillPie(brushes[i], centerX - radius, centerY - radius, 2 * radius, 2 * radius, (float)startAngle, (float)sweepAngle);
                    startAngle += sweepAngle;

            }
        }

        private void DrawCircleNumber(Graphics g, int number, int centerX, int centerY, int radius)
        {
            // Establecer la fuente para los números
            Font font = new Font("Arial", 12, FontStyle.Bold);
            SolidBrush brush = new SolidBrush(Color.Black);

            // Calcular las posiciones para los números
            int textX = centerX - 5;
            int textY = centerY + radius + -9;

            // Dibujar el número
            g.DrawString(number.ToString(), font, brush, textX, textY);

            // Liberar recursos
            font.Dispose();
            brush.Dispose();
        }

        //------------------------------------------------ APARIENCIA CONTROL AREA STAR -----------------------------------------------------------//
        private GraphicsPath GetRoundedPath(Rectangle rect, float radius)
        {
            GraphicsPath path = new GraphicsPath();
            float curveSize = radius * 2F;
            path.StartFigure();
            path.AddArc(rect.X, rect.Y, curveSize, curveSize, 180, 90);
            path.AddArc(rect.Right - curveSize, rect.Y, curveSize, curveSize, 270, 90);
            path.AddArc(rect.Right - curveSize, rect.Bottom - curveSize, curveSize, curveSize, 0, 90);
            path.AddArc(rect.X, rect.Bottom - curveSize, curveSize, curveSize, 90, 90);
            path.CloseFigure();
            return path;
        }
        private void ControlRegionAndBorder(Control control, float radius, Graphics graph, Color borderColor)
        {
            using (GraphicsPath roundPath = GetRoundedPath(control.ClientRectangle, radius))
            using (Pen penBorder = new Pen(borderColor, 1))
            {
                graph.SmoothingMode = SmoothingMode.AntiAlias;
                control.Region = new Region(roundPath);
                graph.DrawPath(penBorder, roundPath);
            }
        }
        private void PnlControl_Paint(object sender, PaintEventArgs e)
        {
            ControlRegionAndBorder(PnlControl, borderRadius, e.Graphics, borderColor);

        }

        ///////////////////////////////////////////////////BOTON Agregar STAR/////////////////////////////////////////////
        private void Pnlbtnclick1_MouseEnter(object sender, EventArgs e)
        {
            Pnlbtnclick1.Visible = false;
            PnlBtnClick2.Visible = true;
            PnlBtnClick3.Visible = false;


        }
        private void PnlBtnClick2_MouseLeave(object sender, EventArgs e)
        {
            Pnlbtnclick1.Visible = true;
            PnlBtnClick2.Visible = false;
            PnlBtnClick3.Visible = false;
        }
        private void PnlBtnClick2_MouseDown(object sender, MouseEventArgs e)
        {
            Pnlbtnclick1.Visible = false;
            PnlBtnClick2.Visible = false;
            PnlBtnClick3.Visible = true;

            ///////////////////////////////////////////////////EVENTO CLICK STAR/////////////////////////////////////////////
            ///
            if (rowIndex >= 0)
            {
                Console.WriteLine("El indice de la fila es: " + rowIndex);

                string miarea = comboBox1.Text;
                string nombre = textBox1.Text;
                CEspacio miespacio = new CEspacio(miarea, nombre); // pasa el tipo de area y nombre del espacio al objeto
                listaEspacios.Insert(rowIndex, miespacio); // agregar el espacio a la tabla, es IMPORTANTE para saber cuantos espacios dibujar

                nombresEspacios.Insert(rowIndex, miespacio.nombre);
                areasEspacios.Insert(rowIndex, miespacio.area);

                // crear una nueva fila y agregar datos a las columnas de esa fila.
                DataRow nuevaFila = dt.NewRow();
                nuevaFila["Area"] = comboBox1.Text;
                nuevaFila["Espacio"] = textBox1.Text;
                // ... Continúa agregando datos a otras columnas según sea necesario.

                // Luego, agrega la nueva fila al DataTable en una ubicación específica, por ejemplo, en la posición 2.

                dt.Rows.InsertAt(nuevaFila, rowIndex);  // El segundo argumento especifica la posición deseada.

                // actualiza el DataTable con la nueva fila.
                dt.AcceptChanges();
                dataGridView1.DataSource = dt;
            }
            else if (rowIndex == -1)
            {
                Console.WriteLine("El indice de la fila es: " + rowIndex);

                dt.Clear();
                for (int i = 0; i < nombresEspacios.Count; i++) // llena el dt
                {
                    dt.Rows.Add(areasEspacios[i], nombresEspacios[i]);
                }
                Console.WriteLine("Cantidad de espacios en listaEspacios: " + listaEspacios.Count);
                dataGridView1.DataSource = dt;

                dt.Clear();
                //Refresh();
                string miarea = comboBox1.Text;
                string nombre = textBox1.Text;
                CEspacio miespacio = new CEspacio(miarea, nombre); // pasa el tipo de area y nombre del espacio al objeto
                listaEspacios.Add(miespacio); // agregar el espacio a la tabla, es IMPORTANTE para saber cuantos espacios dibujar

                nombresEspacios.Add(miespacio.nombre);
                areasEspacios.Add(miespacio.area);

                for (int i = 0; i < nombresEspacios.Count; i++) // llena el dt
                {
                    dt.Rows.Add(areasEspacios[i], nombresEspacios[i]);
                }

                dataGridView1.DataSource = dt; // agregar los datos de la data table a la tabla
                foreach (DataGridViewColumn column in dataGridView1.Columns)
                {
                    column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }


            }

            dibujarMatriz();
            CrearGrafica();
            dataGridView1.ClearSelection();
            rowIndex = -1;

            ///////////////////////////////////////////////////EVENTO CLICK END/////////////////////////////////////////////
        }
        ///////////////////////////////////////////////////BOTON BORRAR END/////////////////////////////////////////////

        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (textBox1.Text == "Ingresa un nombre")
            {
                textBox1.Text = string.Empty;
                textBox1.ForeColor = Color.Black;
            }
        }

        ///////////////////////////////////////////////////BOTON BORRAR STAR/////////////////////////////////////////////
        private void PnlBtnBorrarClick1_MouseEnter(object sender, EventArgs e)
        {
            PnlBtnBorrarClick1.Visible = false;
            PnlBtnBorrarClick2.Visible = true;
            PnlBtnBorrarClick3.Visible = false;

        }
        private void PnlBtnBorrarClick2_MouseLeave(object sender, EventArgs e)
        {
            PnlBtnBorrarClick1.Visible = true;
            PnlBtnBorrarClick2.Visible = false;
            PnlBtnBorrarClick3.Visible = false;
        }
        private void PnlBtnBorrarClick2_MouseDown(object sender, MouseEventArgs e)
        {
            PnlBtnBorrarClick1.Visible = false;
            PnlBtnBorrarClick2.Visible = false;
            PnlBtnBorrarClick3.Visible = true;

            Console.WriteLine("Indice es: " + rowIndex);
            if (rowIndex >= listaEspacios.Count) // comprueba que el indice seleccionado no sea mayor a la cantidad de espacios en la tabla
            {
                rowIndex = 0;
            }
            if (listaEspacios.Count > 0)
            {
                if (rowIndex == -1) rowIndex = 0;
                string valorDeLaCelda = dataGridView1.Rows[rowIndex].Cells["Espacio"].Value.ToString();

                // Luego, puedes realizar las acciones que necesites con la fila o el valor de la celda
                // Por ejemplo, para borrar la fila seleccionada:
                dataGridView1.Rows.RemoveAt(rowIndex);
                nombresEspacios.RemoveAt(rowIndex);
                listaEspacios.RemoveAt(rowIndex);
                areasEspacios.RemoveAt(rowIndex);
                dt.Clear();
                for (int i = 0; i < nombresEspacios.Count; i++) // llena el dt
                {
                    dt.Rows.Add(areasEspacios[i], nombresEspacios[i]);
                }
                Console.WriteLine("Cantidad de espacios en listaEspacios: " + listaEspacios.Count);
                dataGridView1.DataSource = dt;
            }

            dibujarMatriz();
            CrearGrafica();
            dataGridView1.ClearSelection();
            rowIndex = -1;
        }

        ///////////////////////////////////////////////////BOTON Calcular STAR/////////////////////////////////////////////
        private void PnlBtnCalcularClick1_MouseEnter(object sender, EventArgs e)
        {
            PnlBtnCalcularClick1.Visible = false;
            PnlBtnCalcularClick2.Visible = true;
            PnlBtnCalcularClick3.Visible = false;
        }
        private void PnlBtnCalcularClick2_MouseLeave(object sender, EventArgs e)
        {
            PnlBtnCalcularClick1.Visible = true;
            PnlBtnCalcularClick2.Visible = false;
            PnlBtnCalcularClick3.Visible = false;
        }
        private void PnlBtnCalcularClick2_MouseDown(object sender, MouseEventArgs e)
        {
            PnlBtnCalcularClick1.Visible = false;
            PnlBtnCalcularClick2.Visible = false;
            PnlBtnCalcularClick3.Visible = true;

            //genera las sumas de las columnas y filas
            generarSumas();


            //comienza a generar los rangos
            numeros.Clear();
            rangos.Clear();
            Console.WriteLine("ListaTextBoxes empieza en 0s");

            numeros = ConvertirTextBoxAEnteros(listaTextBoxSuma);
            Console.WriteLine("Números originales: " + string.Join(", ", numeros));

            rangos = generarRangos(numeros);
            Console.WriteLine("Rangos asignados: " + string.Join(", ", rangos));

            Console.WriteLine("Convertimos la lista de rangos a listaTextBoxRango");
            ConvertirEnterosATextBox(rangos);
            Console.WriteLine("Los valores de rango han pasado a listaTextBoxRango");

            panel3.Invalidate();

        }
        ///////////////////////////////////////////////////BOTON Calcular END/////////////////////////////////////////////

        ///////////////////////////////////////////////////BOTON BORRAR END/////////////////////////////////////////////

        //------------------------------------------------ APARIENCIA CONTROL AREA END -----------------------------------------------------------//

        

        private void CrearGrafica()
        {
            social = ContarFilasConValor(dt, "Social");
            Console.WriteLine("social = " + social);
            semiSocial = ContarFilasConValor(dt, "Semi-Social");
            Console.WriteLine("social = " + semiSocial);
            servicio = ContarFilasConValor(dt, "Servicio");
            Console.WriteLine("social = " + servicio);
            privado = ContarFilasConValor(dt, "Privada");
            Console.WriteLine("social = " + privado);
            estado = true;
            panel3.Invalidate();

        }

        public int ContarFilasConValor(DataTable dataTable, string valorBuscado)
        {
            int cantidad = dataTable.AsEnumerable()
                .Count(row => row.Field<string>("Area") == valorBuscado);

            return cantidad;
        }
    }
}
