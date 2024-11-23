using System.Drawing;
using System.Threading;

namespace WinFormsSimuladorMk1
{
    public partial class Form1 : Form
    {
        private Thread hilo1, hilo2, hilo3; // Hilos para cada archivo
        private bool pausar1 = false, pausar2 = false, pausar3 = false; // Pausas individuales
        private bool detener1 = false, detener2 = false, detener3 = false; // Detener individuales
        private bool pausarSimulacion = false; // Pausa global
        private int r, g, b;
        private int[] vecTAM = new int[10];
        private static int TAM = 10;

        public Form1()
        {
            InitializeComponent();
            /*this.vecTAM[0] = 20;
            this.vecTAM[1] = 8;
            this.vecTAM[2] = 2;
            this.vecTAM[3] = 10;
            this.vecTAM[4] = 15;
            this.vecTAM[5] = 5;
            this.vecTAM[6] = 19;
            this.vecTAM[7] = 4;
            this.vecTAM[8] = 12;
            this.vecTAM[9] = 5;*/
        }

        private void btnIniciar_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Iniciando la simulación.");
            groupBoxMemoria.Visible = true;
            iniciarSimulacion();
        }

        private void iniciarSimulacion()
        {
            // Iniciar los hilos para cada archivo usando métodos explícitos
            hilo1 = new Thread(SimularPanel1);
            hilo2 = new Thread(SimularPanel2);
            hilo3 = new Thread(SimularPanel3);

            hilo1.Start();
            hilo2.Start();
            hilo3.Start();
        }

        private void SimularPanel1()
        {
            SimularPanel(panelEsp1, labelO1, labelT1, EstaPausado1, Detener1);
        }

        private void SimularPanel2()
        {
            SimularPanel(panelEsp2, labelO2, labelT2, EstaPausado2, Detener2);
        }

        private void SimularPanel3()
        {
            SimularPanel(panelEsp3, labelO3, labelT3, EstaPausado3, Detener3);
        }

        private void SimularPanel(Panel panel, Label label, Label labelT, Func<bool> estaPausado, Func<bool> detener)
        {
            Random alea = new Random();

            while (true)
            {
                int tiempo = alea.Next(10000, 20001);
                if (detener())
                {
                    return; // Salir del hilo de forma segura
                }


                if (!estaPausado())
                {


                    this.Invoke(new Action(() =>
                    {
                        int tamArchivo = alea.Next(5, 15); // Generar tamaño del archivo
                        /* Cambiar color del panel y mostrar tamaño del archivo
                        r = alea.Next(256);
                        g = alea.Next(256);
                        b = alea.Next(256);
                        panel.BackColor = Color.FromArgb(r, g, b);*/
                        panel.BackColor = Color.Red;

                        // Actualizar etiqueta con el tamaño del archivo generado
                        label.Text = $"  {tamArchivo}kb";

                        labelT.Text = $" {tiempo / 1000} seg.";
                    }));
                }
                Thread.Sleep(tiempo); // Simular intervalo de tiempo
            }
        }

        // Métodos explícitos para verificar estados individuales
        private bool EstaPausado1()
        {
            return pausar1 || pausarSimulacion;
        }

        private bool EstaPausado2()
        {
            return pausar2 || pausarSimulacion;
        }

        private bool EstaPausado3()
        {
            return pausar3 || pausarSimulacion;
        }

        private bool Detener1()
        {
            return detener1;
        }

        private bool Detener2()
        {
            return detener2;
        }

        private bool Detener3()
        {
            return detener3;
        }

        private void btnPausaP1_Click(object sender, EventArgs e)
        {
            pausar1 = !pausar1;

            if (pausar1)
            {
                panelEsp1.BackColor = Color.Yellow;
                btnPausaP1.Text = "Reanudar";
            }
            else
            {
                panelEsp1.BackColor = Color.Red;
                btnPausaP1.Text = "Proceso 1";
            }
        }

        private void btnPausaP2_Click(object sender, EventArgs e)
        {
            pausar2 = !pausar2;

            if (pausar2)
            {
                panelEsp2.BackColor = Color.Yellow;
                btnPausaP2.Text = "Reanudar";
            }
            else
            {
                panelEsp2.BackColor = Color.Red;
                btnPausaP2.Text = "Proceso 2";
            }
        }

        private void btnPausaP3_Click(object sender, EventArgs e)
        {
            pausar3 = !pausar3;

            if (pausar3)
            {
                panelEsp3.BackColor = Color.Yellow;
                btnPausaP3.Text = "Reanudar";
            }
            else
            {
                panelEsp3.BackColor = Color.Red;
                btnPausaP3.Text = "Proceso 3";
            }
        }

        private void btnFin_Click(object sender, EventArgs e)
        {
            detener1 = true;
            detener2 = true;
            detener3 = true;

            MessageBox.Show("La simulación ha terminado.");

            panelEsp1.BackColor = Color.White;
            panelEsp2.BackColor = Color.White;
            panelEsp3.BackColor = Color.White;
            labelO1.Text = "  0kb";
            labelO2.Text = "  0kb";
            labelO3.Text = "  0kb";
            labelT1.Text = "   0";
            labelT2.Text = "   0";
            labelT3.Text = "   0";
        }

        private void btnPausa_Click(object sender, EventArgs e)
        {
            pausarSimulacion = !pausarSimulacion;
            string estado;

            if (pausarSimulacion)
            {
                estado = "Pausada";
                btnPausa.Text = "Reanudar";
                panelEsp1.BackColor = Color.Yellow;
                panelEsp2.BackColor = Color.Yellow;
                panelEsp3.BackColor = Color.Yellow;
            }
            else
            {
                estado = "Reanudada";
                btnPausa.Text = "Pausar Simulacion";
                panelEsp1.BackColor = Color.Red;
                panelEsp2.BackColor = Color.Red;
                panelEsp3.BackColor = Color.Red;
            }

            MessageBox.Show($"Simulación {estado}.");
        }

        private void tableMemoria_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
