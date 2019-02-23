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
        private class GenericField
        {
            Grid grid;
            Label label;
            TextBox textBox;

            public GenericField(Panel parent, string name)
            {
                grid = new Grid();
                label = new Label();
                textBox = new TextBox();

                parent.Children.Add(grid);

                grid.Children.Add(label);
                grid.Children.Add(textBox);
                grid.Width = parent.Width;
                grid.Height = 22;
                grid.Margin = new Thickness(0, 2.5, 0, 2.5);

                label.Content = name;
                label.Height = 22;
                label.HorizontalAlignment = HorizontalAlignment.Left;
                label.Padding = new Thickness(0);
                label.VerticalContentAlignment = VerticalAlignment.Center;
                label.UpdateLayout();

                textBox.Margin = new Thickness(label.ActualWidth + 10, 0, 5, 0);
                textBox.TextWrapping = TextWrapping.Wrap;
            }

            public void FixLayout()
            {
                grid.UpdateLayout();
                label.UpdateLayout();
                textBox.UpdateLayout();
                textBox.Margin = new Thickness(label.ActualWidth + 10, 0, 5, 0);
            }
        }

        private class LabeledCheckBox
        {
            Grid grid;
            Label label;
            CheckBox checkBox;

            public LabeledCheckBox(Panel parent, string name)
            {
                grid = new Grid();
                label = new Label();
                checkBox = new CheckBox();

                parent.Children.Add(grid);

                grid.Children.Add(label);
                grid.Children.Add(checkBox);
                grid.Width = parent.Width;
                grid.Height = 22;
                grid.Margin = new Thickness(0, 2.5, 0, 2.5);

                label.Content = name;
                label.Height = 22;
                label.HorizontalAlignment = HorizontalAlignment.Left;
                label.Padding = new Thickness(0);
                label.VerticalContentAlignment = VerticalAlignment.Center;
                label.UpdateLayout();

                checkBox.Margin = new Thickness(label.ActualWidth + 10, 5, 0, 0);
                checkBox.HorizontalAlignment = HorizontalAlignment.Left;
            }

            public void FixLayout()
            {
                grid.UpdateLayout();
                label.UpdateLayout();
                checkBox.UpdateLayout();
                checkBox.Margin = new Thickness(label.ActualWidth + 10, 5, 0, 0);
            }
        }

        private class RseSkillType
        {
            StackPanel typePanel;
            GenericField name;
            Label attLabel;
            StackPanel attPanel;
            LabeledCheckBox strength;
            LabeledCheckBox reflex;
            LabeledCheckBox intelligence;
            Label skillLabel;
            StackPanel skillPanel;
            List<GenericField> skillList;
            Button skillButton;
            Separator separator;

            public RseSkillType(Panel parent)
            {
                typePanel = new StackPanel();
                name = new GenericField(typePanel, "Name:");
                attLabel = new Label();
                attPanel = new StackPanel();
                strength = new LabeledCheckBox(attPanel, "Strength:");
                reflex = new LabeledCheckBox(attPanel, "Reflex:");
                intelligence = new LabeledCheckBox(attPanel, "Intelligence:");
                skillLabel = new Label();
                skillPanel = new StackPanel();
                skillList = new List<GenericField>();
                skillButton = new Button();
                separator = new Separator();

                parent.Children.Add(typePanel);

                typePanel.Children.Add(attLabel);
                typePanel.Children.Add(attPanel);
                typePanel.Children.Add(skillLabel);
                typePanel.Children.Add(skillPanel);
                typePanel.Children.Add(skillButton);
                typePanel.Children.Add(separator);

                name.FixLayout();

                attLabel.Content = "Active Attributes:";
                attLabel.Height = 22;
                attLabel.HorizontalAlignment = HorizontalAlignment.Left;
                attLabel.Padding = new Thickness(0);
                attLabel.VerticalContentAlignment = VerticalAlignment.Center;

                attPanel.Margin = new Thickness(40, 0, 0, 0);

                strength.FixLayout();

                reflex.FixLayout();

                intelligence.FixLayout();

                skillLabel.Content = "Skills:";
                skillLabel.Height = 22;
                skillLabel.HorizontalAlignment = HorizontalAlignment.Left;
                skillLabel.Padding = new Thickness(0);
                skillLabel.VerticalContentAlignment = VerticalAlignment.Center;

                skillPanel.Margin = new Thickness(40, 0, 0, 0);

                skillButton.Width = 75;
                skillButton.Content = "Add New";
                skillButton.Click += btnAddSkill_Click;
            }

            private void btnAddSkill_Click(object sender, RoutedEventArgs e)
            {
                skillList.Add(new GenericField(skillPanel, skillList.Count + ":"));
            }
        }

        private class RseCustomField
        {

        }

        List<RseSkillType> rseSkillTypes;
        List<GenericField> rseVocations;
        List<GenericField> rseProficiencies;

        public RuleSetEditor()
        {
            InitializeComponent();
            rseSkillTypes = new List<RseSkillType>();
            rseVocations = new List<GenericField>();
            rseProficiencies = new List<GenericField>();
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        void saveAs_Click(object sender, RoutedEventArgs e)
        {

        }

        void btnAddNewST_Click(object sender, RoutedEventArgs e)
        {
            rseSkillTypes.Add(new RseSkillType((StackPanel)this.FindName("stkSkillTypes")));
        }

        void btnAddNewVoc_Click(object sender, RoutedEventArgs e)
        {
            rseVocations.Add(new GenericField((StackPanel)this.FindName("stkVocations"), rseVocations.Count + ":"));
        }

        void btnAddNewProf_Click(object sender, RoutedEventArgs e)
        {
            rseProficiencies.Add(new GenericField((StackPanel)this.FindName("stkProficiencies"), rseProficiencies.Count + ":"));
        }
    }
}
