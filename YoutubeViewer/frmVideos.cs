using System;
using System.Globalization;
using System.Windows.Forms;

namespace YoutubeViewer
{
    public partial class frmVideos : Form
    {
        public frmVideos()
        {
            InitializeComponent();
        }

        private void frmVideos_Load(object sender, EventArgs e)
        {
            //Exibição em detalhes
            lvlVideos.View = View.Details;
            //Permite selecionar a linha toda
            lvlVideos.FullRowSelect = true;
            //Exibir as linhas de grade
            lvlVideos.GridLines = true;

            lvlVideos.Columns.Add("Vídeos", 400, HorizontalAlignment.Left);
            lvlVideos.Columns.Add("Horário", 200, HorizontalAlignment.Right);

            txtUrl.Select();
        }

        private void btnCarregar_Click(object sender, EventArgs e)
        {
            //Verificar se o campo URL foi preenchido
            if (txtUrl.Text.Trim().Equals(string.Empty))
            {
                MessageBox.Show("Você deve informar a URL do vídeo!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //Adiciona na listview
            ListViewItem lvi = new ListViewItem(txtUrl.Text.Trim());
            lvi.SubItems.Add(DateTime.Now.ToString("dd/MM/yyyy 'às' HH:mm:ss", new CultureInfo("pt-BR")));
            lvlVideos.Items.Add(lvi);

            //Exibir o vídeo
            string html = "<html><head>";
            html += "<meta content='IE=Edge' http-equiv='X-UA-Compatible'/>";
            html += "<center><iframe id='video' src='https://www.youtube.com/embed/{0}?autoplay={1}' width='{2}' height='{3}' frameborder='0' allowfullscreen='true'></iframe></center>";
            html += "</body></html>";

            wbVideo.DocumentText = string.Format(html, txtUrl.Text.Trim().Split('=')[1], chkAutoplay.Checked ? "1" : "0", wbVideo.Width - 30, wbVideo.Height - 30);

            txtUrl.Text = string.Empty;
            chkAutoplay.Checked = false;
        }
    }
}
