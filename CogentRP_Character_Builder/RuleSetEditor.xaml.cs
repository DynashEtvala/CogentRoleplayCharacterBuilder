using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Newtonsoft.Json;

namespace CogentRP_Character_Builder
{
    /// <summary>
    /// Interaction logic for RuleSetEditor.xaml
    /// </summary>
    public partial class RuleSetEditor : Window
    {
        public RuleSetEditor()
        {
            InitializeComponent();
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        void saveAs_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
