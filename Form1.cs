using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.IO.Image;
using iText.Layout.Element;


namespace CuadroPondercion
{
    public partial class Form1 : Form
    {
        private bool estado = false;
        private int social, semiSocial, servicio, privado;
        int tam;
        public Graphics ZonaDibujo;
        List<TextBox> listaTextBoxes;
        List<TextBox> listaTextBoxSuma;
        List<TextBox> listaTextBoxRango;

        List<CEspacio> listaEspacios;
        List<string> areasEspacios;
        List<string> nombresEspacios;
        List<int> rangos;
        List<int> numeros;
        DataTable dt;

        //Selecciona la posicion del primer rombo

        int Si = 0;
        int Ti = 0;
        int Ui = -30;
        int Vi = 20;
        int Wi = 0;
        int Xi = 40;
        int Yi = 30;
        int Zi = 20;

        int rowIndex = 0;
        public Form1()
        {
            InitializeComponent();
            ZonaDibujo = panel2.CreateGraphics();
            tam = 10;
            listaTextBoxes = new List<TextBox>();
            listaTextBoxSuma = new List<TextBox>();
            listaTextBoxRango = new List<TextBox>();
            listaEspacios = new List<CEspacio>();
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

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (rowIndex>0)
            {
                Console.WriteLine("El indice de la fila es: " + rowIndex);
                // Luego, puedes crear una nueva fila y agregar datos a las columnas de esa fila.
                DataRow nuevaFila = dt.NewRow();
                nuevaFila["Area"] = comboBox1.Text;
                nuevaFila["Espacio"] = textBox1.Text;
                // ... Continúa agregando datos a otras columnas según sea necesario.

                // Luego, agrega la nueva fila al DataTable en una ubicación específica, por ejemplo, en la posición 2.
                dt.Rows.InsertAt(nuevaFila, rowIndex);  // El segundo argumento especifica la posición deseada.

                // Finalmente, puedes actualizar el DataTable con la nueva fila.
                dt.AcceptChanges();
            }
            else
            {
                dt.Clear();
                //Refresh();
                string miarea = comboBox1.Text;
                string nombre = textBox1.Text;
                CEspacio miespacio = new CEspacio(miarea, nombre); // pasa el tipo de area y nombre del espacio al objeto
                listaEspacios.Add(miespacio); // agregar el espacio a la tabla

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
                CrearGrafica();
                
            }
            dibujarMatriz();


        }
        

        private void button2_Click(object sender, EventArgs e)
        {
            //Console.WriteLine("Indice es: " + rowIndex);
            if (rowIndex >= listaEspacios.Count)
            {
                rowIndex = 0;
            }
            if(listaEspacios.Count > 0)
            {
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

                dataGridView1.DataSource = dt;
            }
            dibujarMatriz();
        }

        private void GenerarTxtBox()
        {
            BorrarTxtBox();
            int N = 20;
            int M = 30;

            for (int j = 0; j < tam; j++)
            {

                int n = N;
                int m = M;

                for (int q = tam; q >= j; q--)
                {

                    //Crea las filas de TextBox
                    TextBox text = new TextBox();
                    text.Location = new Point(n, m);
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

        private void dibujarMatriz()
        {
            tam = listaEspacios.Count;
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

        private void button3_Click(object sender, EventArgs e)
        {
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

        private List<int> generarRangos(List<int> numeros)
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

        private void panel3_Paint(object sender, PaintEventArgs e)
        {
            
            if (estado)
            {

                if ((social + semiSocial + servicio + privado) != 0)
                {
                    Graphics grafica = CreateGraphics();

                    int totalPonderaciones = social + semiSocial + servicio + privado;
                    int grados1 = social * 360 / totalPonderaciones;
                    int grados2 = semiSocial * 360 / totalPonderaciones;
                    int grados3 = servicio * 360 / totalPonderaciones;
                    int grados4 = privado * 360 / totalPonderaciones;
                    //Colores con transparencia
                    Color newGreen = Color.FromArgb(40, Color.Green);
                    Color newOrange = Color.FromArgb(40, Color.Orange);
                    Color newYellow = Color.FromArgb(40, Color.Yellow);
                    Color newRed = Color.FromArgb(40, Color.Red);
                    //Dividir la grafica de pastel
                    grafica.FillPie(new SolidBrush(newGreen), 50, 50, 400, 400, 0, grados1);
                    grafica.FillPie(new SolidBrush(newOrange), 500, 200, 400, 400, grados1, grados2);
                    grafica.FillPie(new SolidBrush(newYellow), 100, 100, 400, 400, grados1 + grados2, grados3);
                    grafica.FillPie(new SolidBrush(newRed), 200, 10, 400, 400, grados1 + grados2 + grados3, grados4);

                    //Divisiones de rangos (rango de 5)
                    // Create Pens
                    Pen blackPen = new Pen(Color.Black, 2);
                    // Create a rectangle
                    Rectangle rect = new Rectangle(195, 195, 40, 40);
                    // Draw ellipses
                    e.Graphics.DrawEllipse(blackPen, 165.0F, 165.0F, 100.0F, 100.0F);
                    e.Graphics.DrawEllipse(blackPen, rect);
                    e.Graphics.DrawEllipse(blackPen, 120.0F, 120.0F, 190.0F, 190.0F);
                    e.Graphics.DrawEllipse(blackPen, 70.0F, 70.0F, 290.0F, 290.0F);
                    e.Graphics.DrawEllipse(blackPen, 30.0F, 30.0F, 360.0F, 360.0F);
                    //Dispose of objects
                    blackPen.Dispose();
                    
                }
            }
            
        }

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
            
        }

        public int ContarFilasConValor(DataTable dataTable, string valorBuscado)
        {
            int cantidad = dataTable.AsEnumerable()
                .Count(row => row.Field<string>("Area") == valorBuscado);

            return cantidad;
        }
    }
}
