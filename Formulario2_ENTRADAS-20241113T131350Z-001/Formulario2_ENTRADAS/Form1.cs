using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Formulario_ENTRADAS
{
    public partial class Form1 : Form
    {
        // Clase Producto

        [Serializable]
        public class Producto
        {
            public string Codigo { get; set; }
            public string Nombre { get; set; }
            public string TipoInsumo { get; set; }
            public decimal Precio { get; set; }
            public int Cantidad { get; set; }
        }

        private List<Producto> productos = new List<Producto>();
        private const string archivoProductos = "productos.bin";

        public Form1()
        {
            InitializeComponent();
            CargarProductos();
            ActualizarDataGridView();
            ActualizarTotales();  // Inicializar los totales al inicio
        }

        // Método para cargar productos desde el archivo binario
        private void CargarProductos()
        {
            if (File.Exists(archivoProductos) && new FileInfo(archivoProductos).Length > 0)
            {
                try
                {
                    using (FileStream fs = new FileStream(archivoProductos, FileMode.Open))
                    {
                        BinaryFormatter bf = new BinaryFormatter();
                        productos = (List<Producto>)bf.Deserialize(fs);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar productos: " + ex.Message);
                    productos = new List<Producto>(); // Crear lista vacía en caso de error
                }
            }
        }

        // Método para guardar productos en el archivo binario
        private void GuardarProductos()
        {
            using (FileStream fs = new FileStream(archivoProductos, FileMode.Create))
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(fs, productos);
            }
        }

        // Método para actualizar el DataGridView
        private void ActualizarDataGridView()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = productos;
        }

        // Método para limpiar las cajas de texto
        private void LimpiarCampos()
        {
            textBox1.Text = string.Empty;  // Código de producto
            textBox2.Text = string.Empty;  // Nombre del producto
            textBox7.Text = string.Empty;  // Tipo de insumo
            textBox3.Text = string.Empty;  // Precio
            textBox4.Text = string.Empty;  // Cantidad
        }

        // Método para actualizar los totales en la otra pestaña
        private void ActualizarTotales()
        {
            int totalProductos = 0;
            decimal totalPrecio = 0;
            decimal totalInsumosMateriaPrima = 0;
            decimal totalInsumosProductoFinal = 0;

            foreach (var producto in productos)
            {
                totalProductos += producto.Cantidad;
                totalPrecio += producto.Precio * producto.Cantidad;

                if (producto.TipoInsumo == "Materia Prima")
                {
                    totalInsumosMateriaPrima += producto.Precio * producto.Cantidad;
                }
                else if (producto.TipoInsumo == "Producto Final")
                {
                    totalInsumosProductoFinal += producto.Precio * producto.Cantidad;
                }
            }

            // Actualizamos los TextBox en la segunda pestaña con los totales
            textBox5.Text = totalProductos.ToString();  // Total productos
            textBox6.Text = totalPrecio.ToString("C");  // Total precio (formato monetario)
            textBox8.Text = totalInsumosMateriaPrima.ToString("C");  // Total insumos materia prima
            textBox9.Text = totalInsumosProductoFinal.ToString("C");  // Total insumos producto final
        }

        
        // Evento Click para el botón Buscar
        private void button2_Click_1(object sender, EventArgs e)
        {
            string codigo = textBox1.Text;
            Producto producto = productos.Find(p => p.Codigo == codigo);
            if (producto != null)
            {
                textBox2.Text = producto.Nombre;
                textBox7.Text = producto.TipoInsumo;
                textBox3.Text = producto.Precio.ToString();
                textBox4.Text = producto.Cantidad.ToString();
            }
            else
            {
                MessageBox.Show("Producto no encontrado.");
            }
        }

        // Evento Click para el botón Editar
        private void button1_Click_1(object sender, EventArgs e)
        {
            Producto producto = productos.Find(p => p.Codigo == textBox1.Text);
            if (producto != null)
            {
                if (string.IsNullOrWhiteSpace(textBox2.Text) || string.IsNullOrWhiteSpace(textBox7.Text) ||
                    string.IsNullOrWhiteSpace(textBox3.Text) || string.IsNullOrWhiteSpace(textBox4.Text))
                {
                    MessageBox.Show("Por favor, completa todos los campos.");
                    return;
                }

                if (!decimal.TryParse(textBox3.Text, out decimal precio))
                {
                    MessageBox.Show("El precio debe ser un número decimal.");
                    return;
                }

                if (!int.TryParse(textBox4.Text, out int cantidad))
                {
                    MessageBox.Show("La cantidad debe ser un número entero.");
                    return;
                }

                producto.Nombre = textBox2.Text;
                producto.TipoInsumo = textBox7.Text;
                producto.Precio = precio;
                producto.Cantidad = cantidad;

                GuardarProductos();
                ActualizarDataGridView();
                ActualizarTotales();  // Actualizar los totales
                LimpiarCampos();
                MessageBox.Show("Producto editado exitosamente.");
            }
            else
            {
                MessageBox.Show("Producto no encontrado.");
            }
        }

        // Método para inicializar el DataGridView con las columnas adecuadas
        private void InicializarDataGridView()
        {
            dataGridView1.ColumnCount = 5;
            dataGridView1.Columns[0].Name = "Código Producto";
            dataGridView1.Columns[1].Name = "Nombre Producto";
            dataGridView1.Columns[2].Name = "Tipo de Insumo";
            dataGridView1.Columns[3].Name = "Precio";
            dataGridView1.Columns[4].Name = "Cantidad";
        }

        // Evento Click para el botón Guardar
        private void button4_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text) ||
                string.IsNullOrWhiteSpace(textBox7.Text) || string.IsNullOrWhiteSpace(textBox3.Text) ||
                string.IsNullOrWhiteSpace(textBox4.Text))
            {
                MessageBox.Show("Por favor, completa todos los campos.");
                return;
            }

            if (!decimal.TryParse(textBox3.Text, out decimal precio))
            {
                MessageBox.Show("El precio debe ser un número decimal.");
                return;
            }

            if (!int.TryParse(textBox4.Text, out int cantidad))
            {
                MessageBox.Show("La cantidad debe ser un número entero.");
                return;
            }

            Producto producto = new Producto
            {
                Codigo = textBox1.Text,
                Nombre = textBox2.Text,
                TipoInsumo = textBox7.Text,
                Precio = precio,
                Cantidad = cantidad
            };

            productos.Add(producto);
            GuardarProductos();
            ActualizarDataGridView();
            ActualizarTotales();  // Actualizar los totales
            LimpiarCampos();
            MessageBox.Show("Producto guardado exitosamente.");
        }

        // Evento Click para el botón Eliminar
        private void button3_Click(object sender, EventArgs e)
        {
            Producto producto = productos.Find(p => p.Codigo == textBox1.Text);
            if (producto != null)
            {
                productos.Remove(producto);
                GuardarProductos();
                ActualizarDataGridView();
                ActualizarTotales();  // Actualizar los totales
                LimpiarCampos();
                MessageBox.Show("Producto eliminado exitosamente.");
            }
            else
            {
                MessageBox.Show("Producto no encontrado.");
            }
        }
    }
}

