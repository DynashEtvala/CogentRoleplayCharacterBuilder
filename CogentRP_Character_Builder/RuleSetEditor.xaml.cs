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
        /// <summary>
        /// A TextBox with a preceding Label.
        /// </summary>
        private class LabeledTextBox
        {
            //--------------------------------------------
            //Variables
            //--------------------------------------------

            Grid grid;
            Label label;
            TextBox textBox;

            //--------------------------------------------
            //Constructors
            //--------------------------------------------

            public LabeledTextBox(Panel parent, string name)
            {
                //Initialize Variables
                grid = new Grid();
                label = new Label();
                textBox = new TextBox();

                //Add Grid to Parent Panel
                parent.Children.Add(grid);

                //Define Grid and Add Children
                grid.Children.Add(label);
                grid.Children.Add(textBox);
                grid.Width = parent.Width;
                grid.Height = 22;
                grid.Margin = new Thickness(0, 2.5, 0, 2.5);

                //Define Label UI Element
                label.Content = name;
                label.Height = 22;
                label.HorizontalAlignment = HorizontalAlignment.Left;
                label.Padding = new Thickness(0);
                label.VerticalContentAlignment = VerticalAlignment.Center;
                label.UpdateLayout();

                //Define TextBox UI Element
                textBox.Margin = new Thickness(label.ActualWidth + 10, 0, 5, 0);
                textBox.TextWrapping = TextWrapping.Wrap;
            }

            //--------------------------------------------
            //Functions
            //--------------------------------------------

                ///<summary>
                ///Updates the layouts of UI elements to align properly.
                ///</summary>
            public void FixLayout() 
            {
                grid.UpdateLayout();
                label.UpdateLayout();
                textBox.UpdateLayout();
                textBox.Margin = new Thickness(label.ActualWidth + 10, 0, 5, 0);
            }

            //--------------------------------------------
            //Properties
            //--------------------------------------------

                /// <summary>
                /// Gets or sets the text contents of the text box.
                /// </summary>
            public string Text
            {
                get { return textBox.Text; }
                set { textBox.Text = value; }
            }
        }

        /// <summary>
        /// A CheckBox with a preceding Label.
        /// </summary>
        private class LabeledCheckBox
        {
            //--------------------------------------------
            //Variables
            //--------------------------------------------

            Grid grid;
            Label label;
            CheckBox checkBox;

            //--------------------------------------------
            //Constructors
            //--------------------------------------------

            public LabeledCheckBox(Panel parent, string name)
            {
                //Initialize Variables
                grid = new Grid();
                label = new Label();
                checkBox = new CheckBox();

                //Add Grid to parent Panel
                parent.Children.Add(grid);

                //Define Grid and add Children
                grid.Children.Add(label);
                grid.Children.Add(checkBox);
                grid.Width = parent.Width;
                grid.Height = 22;
                grid.Margin = new Thickness(0, 2.5, 0, 2.5);

                //Define Label
                label.Content = name;
                label.Height = 22;
                label.HorizontalAlignment = HorizontalAlignment.Left;
                label.Padding = new Thickness(0);
                label.VerticalContentAlignment = VerticalAlignment.Center;
                label.UpdateLayout();

                //Define Checkbox
                checkBox.Margin = new Thickness(label.ActualWidth + 10, 5, 0, 0);
                checkBox.HorizontalAlignment = HorizontalAlignment.Left;
            }

            //--------------------------------------------
            //Functions
            //--------------------------------------------

            /// <summary>
            /// Updates the layouts of UI elements to align properly.
            /// </summary>
            public void FixLayout()
            {
                grid.UpdateLayout();
                label.UpdateLayout();
                checkBox.UpdateLayout();
                checkBox.Margin = new Thickness(label.ActualWidth + 10, 5, 0, 0);
            }

            //--------------------------------------------
            //Properties
            //--------------------------------------------

            /// <summary>
            /// Gets or sets whether the checkbox is checked.
            /// </summary>
            public bool? IsChecked
            {
                get { return checkBox.IsChecked; }
                set { checkBox.IsChecked = value; }
            }
        }

        /// <summary>
        /// A block of UI elements representing a skill type and it's respective skills.
        /// </summary>
        private class RseSkillType
        {
            //--------------------------------------------
            //Variables
            //--------------------------------------------

            Label typeNum;
            StackPanel typePanel;
            LabeledTextBox name;
            Separator separator1;
            Label attLabel;
            StackPanel attPanel;
            LabeledCheckBox strength;
            LabeledCheckBox reflex;
            LabeledCheckBox intelligence;
            Separator separator2;
            Label skillLabel;
            StackPanel skillPanel;
            List<LabeledTextBox> skillList;
            Button skillButton;
            Separator separator3;

            //--------------------------------------------
            //Constructors
            //--------------------------------------------

            public RseSkillType(Panel parent, int count)
            {
                //Initialize Variables
                typeNum = new Label();
                typePanel = new StackPanel();
                name = new LabeledTextBox(typePanel, "Name:");
                separator1 = new Separator();
                attLabel = new Label();
                attPanel = new StackPanel();
                strength = new LabeledCheckBox(attPanel, "Strength:");
                reflex = new LabeledCheckBox(attPanel, "Reflex:");
                intelligence = new LabeledCheckBox(attPanel, "Intelligence:");
                separator2 = new Separator();
                skillLabel = new Label();
                skillPanel = new StackPanel();
                skillList = new List<LabeledTextBox>();
                skillButton = new Button();
                separator3 = new Separator();

                //Add relevent elements to parent panel
                parent.Children.Add(typeNum);
                parent.Children.Add(typePanel);
                parent.Children.Add(separator3);

                //Define the index label of the RseSkillType
                typeNum.Content = count + ":";
                typeNum.Height = 22;
                typeNum.HorizontalAlignment = HorizontalAlignment.Left;
                typeNum.Padding = new Thickness(0);
                typeNum.VerticalContentAlignment = VerticalAlignment.Center;

                //Define and add the children of the organizational stack panel
                typePanel.Margin = new Thickness(40, 0, 0, 0);
                typePanel.Children.Add(separator1);
                typePanel.Children.Add(attLabel);
                typePanel.Children.Add(attPanel);
                typePanel.Children.Add(separator2);
                typePanel.Children.Add(skillLabel);
                typePanel.Children.Add(skillPanel);
                typePanel.Children.Add(skillButton);

                //Update the layout of the name textbox
                name.FixLayout();

                //Define the attributes label
                attLabel.Content = "Active Attributes:";
                attLabel.Height = 22;
                attLabel.HorizontalAlignment = HorizontalAlignment.Left;
                attLabel.Padding = new Thickness(0);
                attLabel.VerticalContentAlignment = VerticalAlignment.Center;

                //Define the format panel for attribute checkboxes
                attPanel.Margin = new Thickness(40, 0, 0, 0);

                //Update the layout of attribute checkboxes
                strength.FixLayout();

                reflex.FixLayout();

                intelligence.FixLayout();

                //Define the skill list label
                skillLabel.Content = "Skills:";
                skillLabel.Height = 22;
                skillLabel.HorizontalAlignment = HorizontalAlignment.Left;
                skillLabel.Padding = new Thickness(0);
                skillLabel.VerticalContentAlignment = VerticalAlignment.Center;

                //Define the format panel for the skill list
                skillPanel.Margin = new Thickness(40, 0, 0, 0);

                //Define the button for adding new skills to the skill list
                skillButton.Width = 75;
                skillButton.Content = "Add New";
                skillButton.Click += btnAddSkill_Click;
            }

            //--------------------------------------------
            //Button Functions
            //--------------------------------------------

                /// <summary>
                /// Adds a new LabeledTextBox to the skillList and skillPanel when called.
                /// </summary>
                /// <param name="sender"></param>
                /// <param name="e"></param>
            private void btnAddSkill_Click(object sender, RoutedEventArgs e)
            {
                skillList.Add(new LabeledTextBox(skillPanel, skillList.Count + ":"));
            }
        }

        /// <summary>
        /// A block of UI elements representing a custom stat field.
        /// </summary>
        private class RseCustomField
        {
            //--------------------------------------------
            //Variables
            //--------------------------------------------

            Label fieldNum;
            StackPanel fieldPanel;
            LabeledTextBox name;
            LabeledTextBox defVal;
            LabeledCheckBox isOptional;
            Separator separator;

            //--------------------------------------------
            //Constructors
            //--------------------------------------------

            public RseCustomField(Panel parent, int count)
            {
                fieldNum = new Label();
                fieldPanel = new StackPanel();
                name = new LabeledTextBox(fieldPanel, "Name:");
                defVal = new LabeledTextBox(fieldPanel, "Default Value:");
                isOptional = new LabeledCheckBox(fieldPanel, "Field is Optional:");
                separator = new Separator();

                parent.Children.Add(fieldNum);
                parent.Children.Add(fieldPanel);
                parent.Children.Add(separator);

                fieldNum.Content = count + ":";
                fieldNum.Height = 22;
                fieldNum.HorizontalAlignment = HorizontalAlignment.Left;
                fieldNum.Padding = new Thickness(0);
                fieldNum.VerticalContentAlignment = VerticalAlignment.Center;

                fieldPanel.Margin = new Thickness(40, 0, 0, 0);

                name.FixLayout();

                defVal.FixLayout();

                isOptional.FixLayout();
            }
        }

        private class RseDisablingCharacteristics
        {
            public class RseSkillTypeModifier
            {
                Label modNum;
                StackPanel modPanel;
                LabeledTextBox typeIndex;
                LabeledTextBox diceReduction;
                LabeledTextBox spCostMult;
                Separator separator;

                RseSkillTypeModifier(Panel parent, int count)
                {
                    modNum = new Label();
                    modPanel = new StackPanel();
                    typeIndex = new LabeledTextBox(modPanel, "Skill Type Index:");
                    diceReduction = new LabeledTextBox(modPanel, "Dice Reduction:");
                    spCostMult = new LabeledTextBox(modPanel, "Skill Cost Multiplier:");
                    separator = new Separator();

                    parent.Children.Add(modNum);
                    parent.Children.Add(modPanel);
                    parent.Children.Add(separator);

                    modNum.Content = count + ":";
                    modNum.Height = 22;
                    modNum.HorizontalAlignment = HorizontalAlignment.Left;
                    modNum.Padding = new Thickness(0);
                    modNum.VerticalContentAlignment = VerticalAlignment.Center;

                    modPanel.Margin = new Thickness(40, 0, 0, 0);

                    typeIndex.FixLayout();

                    diceReduction.FixLayout();

                    spCostMult.FixLayout();
                }
            }
            Label charNum;
            StackPanel charPanel;
            LabeledTextBox name;
            LabeledTextBox spVal;



        }

        List<RseSkillType> rseSkillTypes;
        List<LabeledTextBox> rseVocations;
        List<LabeledTextBox> rseProficiencies;
        List<RseCustomField> rseCustomFields;

        public RuleSetEditor()
        {
            InitializeComponent();
            rseSkillTypes = new List<RseSkillType>();
            rseVocations = new List<LabeledTextBox>();
            rseProficiencies = new List<LabeledTextBox>();
            rseCustomFields = new List<RseCustomField>();
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        void saveAs_Click(object sender, RoutedEventArgs e)
        {

        }

        void btnAddNewST_Click(object sender, RoutedEventArgs e)
        {
            rseSkillTypes.Add(new RseSkillType((StackPanel)this.FindName("stkSkillTypes"), rseSkillTypes.Count));
        }

        void btnAddNewVoc_Click(object sender, RoutedEventArgs e)
        {
            rseVocations.Add(new LabeledTextBox((StackPanel)this.FindName("stkVocations"), rseVocations.Count + ":"));
        }

        void btnAddNewProf_Click(object sender, RoutedEventArgs e)
        {
            rseProficiencies.Add(new LabeledTextBox((StackPanel)this.FindName("stkProficiencies"), rseProficiencies.Count + ":"));
        }

        void btnAddNewCustomField_Click(object sender, RoutedEventArgs e)
        {
            rseCustomFields.Add(new RseCustomField((StackPanel)this.FindName("stkCustomFields"), rseCustomFields.Count));
        }
    }
}
