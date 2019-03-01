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
        #region Sub-Classes
        /// <summary>
        /// A TextBox with a preceding Label.
        /// </summary>
        private class LabeledTextBox
        {
            //--------------------------------------------
            //Variables
            //--------------------------------------------

            Panel parent;
            Grid grid;
            Label label;
            TextBox textBox;

            //--------------------------------------------
            //Constructors
            //--------------------------------------------

            public LabeledTextBox(Panel _parent, string name)
            {
                //Initialize Variables
                parent = _parent;
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

            /// <summary>
            /// Removes all parent/child connections within and to this object.
            /// </summary>
            public void Remove()
            {
                parent.Children.Remove(grid);
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

            Panel parent;
            Grid grid;
            Label label;
            CheckBox checkBox;

            //--------------------------------------------
            //Constructors
            //--------------------------------------------

            public LabeledCheckBox(Panel _parent, string name)
            {
                //Initialize Variables
                parent = _parent;
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

            /// <summary>
            /// Removes all parent/child connections within and to this object.
            /// </summary>
            public void Remove()
            {
                parent.Children.RemoveAt(0);
            }

            //--------------------------------------------
            //Properties
            //--------------------------------------------

            /// <summary>
            /// Gets or sets whether the checkbox is checked.
            /// </summary>
            public bool IsChecked
            {
                get { return (bool)checkBox.IsChecked; }
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

            Panel parent;
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
            Grid skillButtonGrid;
            Button addSkillButton;
            Button removeSkillButton;
            Separator separator3;

            //--------------------------------------------
            //Constructors
            //--------------------------------------------

            public RseSkillType(Panel _parent, int count)
            {
                //Initialize Variables
                parent = _parent;
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
                skillButtonGrid = new Grid();
                addSkillButton = new Button();
                removeSkillButton = new Button();
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
                typePanel.Children.Add(skillButtonGrid);

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

                //Define and add children for Button Grid
                skillButtonGrid.Height = 22;
                skillButtonGrid.ColumnDefinitions.Add(new ColumnDefinition());
                skillButtonGrid.ColumnDefinitions.Add(new ColumnDefinition());
                skillButtonGrid.Children.Add(addSkillButton);
                skillButtonGrid.Children.Add(removeSkillButton);

                //Define the button for adding new skills to the skill list
                addSkillButton.Width = 75;
                addSkillButton.Content = "Add New";
                addSkillButton.Click += btnAddSkill_Click;
                Grid.SetColumn(addSkillButton, 0);

                //Define the button for removing skills from the skill list
                removeSkillButton.Width = 75;
                removeSkillButton.Content = "Remove";
                removeSkillButton.Click += btnRemoveSkill_Click;
                Grid.SetColumn(removeSkillButton, 1);
            }

            //--------------------------------------------
            //Functions
            //--------------------------------------------

            /// <summary>
            /// Removes all parent/child connections within and to this object.
            /// </summary>
            public void Remove()
            {
                parent.Children.Remove(typeNum);
                parent.Children.Remove(typePanel);
                parent.Children.Remove(separator3);
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

            private void btnRemoveSkill_Click(object sender, RoutedEventArgs e)
            {
                skillList[skillList.Count - 1].Remove();
                skillList.RemoveAt(skillList.Count - 1);
            }

            //--------------------------------------------
            //Properties
            //--------------------------------------------

            /// <summary>
            /// Gets or sets the contents of the Name TextBox.
            /// </summary>
            public string Name
            {
                get { return name.Text; }
                set { name.Text = value; }
            }

            /// <summary>
            /// Gets or sets whether the strength CheckBox is checked.
            /// </summary>
            public bool Strength
            {
                get { return strength.IsChecked; }
                set { strength.IsChecked = value; }
            }

            /// <summary>
            /// Gets or sets whether the reflex CheckBox is checked.
            /// </summary>
            public bool Reflex
            {
                get { return reflex.IsChecked; }
                set { reflex.IsChecked = value; }
            }

            /// <summary>
            /// Gets or sets whether the intelligence CheckBox is checked.
            /// </summary>
            public bool Intelligence
            {
                get { return intelligence.IsChecked; }
                set { intelligence.IsChecked = value; }
            }

            public List<LabeledTextBox> Skills
            {
                get { return skillList; }
                set { skillList = value; }
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

            Panel parent;
            Label fieldNum;
            StackPanel fieldPanel;
            LabeledTextBox name;
            LabeledTextBox defVal;
            LabeledCheckBox isOptional;
            Separator separator;

            //--------------------------------------------
            //Constructors
            //--------------------------------------------

            public RseCustomField(Panel _parent, int count)
            {
                //Initialize Variables
                parent = _parent;
                fieldNum = new Label();
                fieldPanel = new StackPanel();
                name = new LabeledTextBox(fieldPanel, "Name:");
                defVal = new LabeledTextBox(fieldPanel, "Default Value:");
                isOptional = new LabeledCheckBox(fieldPanel, "Field is Optional:");
                separator = new Separator();

                //Add relevent elements to parent panel
                parent.Children.Add(fieldNum);
                parent.Children.Add(fieldPanel);
                parent.Children.Add(separator);

                //Define Index Label
                fieldNum.Content = count + ":";
                fieldNum.Height = 22;
                fieldNum.HorizontalAlignment = HorizontalAlignment.Left;
                fieldNum.Padding = new Thickness(0);
                fieldNum.VerticalContentAlignment = VerticalAlignment.Center;

                //Define Panel for UI elements
                fieldPanel.Margin = new Thickness(40, 0, 0, 0);

                //Update layout for UI fields
                name.FixLayout();

                defVal.FixLayout();

                isOptional.FixLayout();
            }

            //--------------------------------------------
            //Functions
            //--------------------------------------------

            /// <summary>
            /// Removes all parent/child connections within and to this object.
            /// </summary>
            public void Remove()
            {
                parent.Children.Remove(fieldNum);
                parent.Children.Remove(fieldPanel);
                parent.Children.Remove(separator);
            }

            //--------------------------------------------
            //Properties
            //--------------------------------------------

            /// <summary>
            /// Gets or sets the contents of the Name TextBox.
            /// </summary>
            public string Name
            {
                get { return name.Text; }
                set { name.Text = value; }
            }

            /// <summary>
            /// Gets or sets the contents of the Default Value TextBox.
            /// </summary>
            public string DefaultValue
            {
                get { return defVal.Text; }
                set { defVal.Text = value; }
            }

            /// <summary>
            /// Gets or sets whether the strength CheckBox is checked.
            /// </summary>
            public bool IsOptional
            {
                get { return isOptional.IsChecked; }
                set { isOptional.IsChecked = value; }
            }
        }

        /// <summary>
        /// A block of UI elements representing a disabling characteristic.
        /// </summary>
        private class RseDisablingCharacteristic
        {
            #region Sub-Classes
            /// <summary>
            /// A block of UI elements within an RseDisablingCharacteristic representing modifiers to a specific Skill Type.
            /// </summary>
            public class RseSkillTypeModifier
            {
                //--------------------------------------------
                //Variables
                //--------------------------------------------

                Panel parent;
                Label modNum;
                StackPanel modPanel;
                LabeledTextBox typeIndex;
                LabeledTextBox diceReduction;
                LabeledTextBox spCostMult;
                Separator separator;

                //--------------------------------------------
                //Constructors
                //--------------------------------------------

                public RseSkillTypeModifier(Panel _parent, int count)
                {
                    //Initialize Variables
                    parent = _parent;
                    modNum = new Label();
                    modPanel = new StackPanel();
                    typeIndex = new LabeledTextBox(modPanel, "Skill Type Index:");
                    diceReduction = new LabeledTextBox(modPanel, "Dice Reduction:");
                    spCostMult = new LabeledTextBox(modPanel, "Skill Cost Multiplier:");
                    separator = new Separator();

                    //Add relevent elements to parent Panel
                    parent.Children.Add(modNum);
                    parent.Children.Add(modPanel);
                    parent.Children.Add(separator);

                    //Define index Label
                    modNum.Content = count + ":";
                    modNum.Height = 22;
                    modNum.HorizontalAlignment = HorizontalAlignment.Left;
                    modNum.Padding = new Thickness(0);
                    modNum.VerticalContentAlignment = VerticalAlignment.Center;

                    //Define Panel for UI elements
                    modPanel.Margin = new Thickness(40, 0, 0, 0);

                    //Update layout of UI elements
                    typeIndex.FixLayout();

                    diceReduction.FixLayout();

                    spCostMult.FixLayout();
                }

                //--------------------------------------------
                //Functions
                //--------------------------------------------

                public void Remove()
                {
                    parent.Children.Remove(modNum);
                    parent.Children.Remove(modPanel);
                    parent.Children.Remove(separator);
                }

                //--------------------------------------------
                //Properties
                //--------------------------------------------

                /// <summary>
                /// Gets or sets the contents of the Skill Type Index TextBox.
                /// </summary>
                public string SkillTypeIndex
                {
                    get { return typeIndex.Text; }
                    set { typeIndex.Text = value; }
                }

                /// <summary>
                /// Gets or sets the contents of the Dice Reduction TextBox.
                /// </summary>
                public string DiceReduction
                {
                    get { return diceReduction.Text; }
                    set { diceReduction.Text = value; }
                }

                /// <summary>
                /// Gets or sets the contents of the Skill Point Cost Multiplier TextBox.
                /// </summary>
                public string SkillPointCostMultiplier
                {
                    get { return spCostMult.Text; }
                    set { spCostMult.Text = value; }
                }
            }

            /// <summary>
            /// A block of UI elements within an RseDisablingCharacteristic representing modifiers to a specific Skill within a SkillType. 
            /// </summary>
            public class RseSkillModifier
            {
                //--------------------------------------------
                //Variables
                //--------------------------------------------

                Panel parent;
                Label modNum;
                StackPanel modPanel;
                LabeledTextBox typeIndex;
                LabeledTextBox skillIndex;
                LabeledTextBox diceReduction;
                LabeledTextBox spCostMult;
                Separator separator;

                //--------------------------------------------
                //Constructors
                //--------------------------------------------

                public RseSkillModifier(Panel _parent, int count)
                {
                    //Initialize Variables
                    parent = _parent;
                    modNum = new Label();
                    modPanel = new StackPanel();
                    typeIndex = new LabeledTextBox(modPanel, "Skill Type Index:");
                    skillIndex = new LabeledTextBox(modPanel, "Skill Index:");
                    diceReduction = new LabeledTextBox(modPanel, "Dice Reduction:");
                    spCostMult = new LabeledTextBox(modPanel, "Skill Cost Multiplier:");
                    separator = new Separator();

                    //Add relevent elements to parent Panel
                    parent.Children.Add(modNum);
                    parent.Children.Add(modPanel);
                    parent.Children.Add(separator);

                    //Define index Label
                    modNum.Content = count + ":";
                    modNum.Height = 22;
                    modNum.HorizontalAlignment = HorizontalAlignment.Left;
                    modNum.Padding = new Thickness(0);
                    modNum.VerticalContentAlignment = VerticalAlignment.Center;

                    //Define Panel for UI elements
                    modPanel.Margin = new Thickness(40, 0, 0, 0);

                    //Update layout of UI elements
                    typeIndex.FixLayout();

                    skillIndex.FixLayout();

                    diceReduction.FixLayout();

                    spCostMult.FixLayout();
                }

                //--------------------------------------------
                //Functions
                //--------------------------------------------

                public void Remove()
                {
                    parent.Children.Remove(modNum);
                    parent.Children.Remove(modPanel);
                    parent.Children.Remove(separator);
                }

                //--------------------------------------------
                //Properties
                //--------------------------------------------

                /// <summary>
                /// Gets or sets the contents of the Skill Type Index TextBox.
                /// </summary>
                public string SkillTypeIndex
                {
                    get { return typeIndex.Text; }
                    set { typeIndex.Text = value; }
                }

                /// <summary>
                /// Gets or sets the contents of the Skill Index TextBox.
                /// </summary>
                public string SkillIndex
                {
                    get { return skillIndex.Text; }
                    set { skillIndex.Text = value; }
                }

                /// <summary>
                /// Gets or sets the contents of the Dice Reduction TextBox.
                /// </summary>
                public string DiceReduction
                {
                    get { return diceReduction.Text; }
                    set { diceReduction.Text = value; }
                }

                /// <summary>
                /// Gets or sets the contents of the Skill Point Cost Multiplier TextBox.
                /// </summary>
                public string SkillPointCostMultiplier
                {
                    get { return spCostMult.Text; }
                    set { spCostMult.Text = value; }
                }
            }

            /// <summary>
            /// A block of UI elements within an RseDisablingCharacteristic representing modifiers to a CustomField.
            /// </summary>
            public class RseCustomFieldModifier
            {
                //--------------------------------------------
                //Variables
                //--------------------------------------------

                Panel parent;
                Label modNum;
                StackPanel modPanel;
                LabeledTextBox fieldIndex;
                LabeledTextBox valMod;
                LabeledCheckBox disableField;
                Separator separator;

                //--------------------------------------------
                //Constructors
                //--------------------------------------------

                public RseCustomFieldModifier(Panel _parent, int count)
                {
                    //Initialize Variables
                    parent = _parent;
                    modNum = new Label();
                    modPanel = new StackPanel();
                    fieldIndex = new LabeledTextBox(modPanel, "Custom Field Index:");
                    valMod = new LabeledTextBox(modPanel, "Value Modifier:");
                    disableField = new LabeledCheckBox(modPanel, "Disable Field:");
                    separator = new Separator();

                    //Add relevent elements to parent Panel
                    parent.Children.Add(modNum);
                    parent.Children.Add(modPanel);
                    parent.Children.Add(separator);

                    //Define index Label
                    modNum.Content = count + ":";
                    modNum.Height = 22;
                    modNum.HorizontalAlignment = HorizontalAlignment.Left;
                    modNum.Padding = new Thickness(0);
                    modNum.VerticalContentAlignment = VerticalAlignment.Center;

                    //Define Panel for UI elements
                    modPanel.Margin = new Thickness(40, 0, 0, 0);

                    //Update layout of UI elements
                    fieldIndex.FixLayout();

                    valMod.FixLayout();

                    disableField.FixLayout();
                }

                //--------------------------------------------
                //Functions
                //--------------------------------------------

                public void Remove()
                {
                    parent.Children.Remove(modNum);
                    parent.Children.Remove(modPanel);
                    parent.Children.Remove(separator);
                }

                //--------------------------------------------
                //Properties
                //--------------------------------------------

                /// <summary>
                /// Gets or sets the contents of the Custom Field Index TextBox.
                /// </summary>
                public string CustomFieldIndex
                {
                    get { return fieldIndex.Text; }
                    set { fieldIndex.Text = value; }
                }

                /// <summary>
                /// Gets or sets the contents of the Value Modifier TextBox.
                /// </summary>
                public string ValueModifier
                {
                    get { return valMod.Text; }
                    set { valMod.Text = value; }
                }

                /// <summary>
                /// Gets or sets whether the strength CheckBox is checked.
                /// </summary>
                public bool DisableField
                {
                    get { return disableField.IsChecked; }
                    set { disableField.IsChecked = value; }
                }
            }
            #endregion

            //--------------------------------------------
            //Variables
            //--------------------------------------------

            Panel parent;
            Label disCharNum;
            StackPanel disCharPanel;
            LabeledTextBox name;
            LabeledTextBox spVal;
            Separator separator1;
            Label skillTypeModLabel;
            StackPanel skillTypeModPanel;
            List<RseSkillTypeModifier> rseSkillTypeModifiers;
            Grid skillTypeModButtonGrid;
            Button addSkillTypeModButton;
            Button removeSkillTypeModButton;
            Separator separator2;
            Label skillModLabel;
            StackPanel skillModPanel;
            List<RseSkillModifier> rseSkillModifiers;
            Grid skillModButtonGrid;
            Button addSkillModButton;
            Button removeSkillModButton;
            Separator separator3;
            Label customFieldModLabel;
            StackPanel customFieldModPanel;
            List<RseCustomFieldModifier> rseCustomFieldModifiers;
            Grid customFieldModButtonGrid;
            Button addCustomFieldModButton;
            Button removeCustomFieldModButton;
            Separator separator4;

            //--------------------------------------------
            //Constructors
            //--------------------------------------------

            public RseDisablingCharacteristic(Panel _parent, int count)
            {
                //Initialize Variables
                parent = _parent;
                disCharNum = new Label();
                disCharPanel = new StackPanel();
                name = new LabeledTextBox(disCharPanel, "Name:");
                spVal = new LabeledTextBox(disCharPanel, "Skill Point Value:");
                separator1 = new Separator();
                skillTypeModLabel = new Label();
                skillTypeModPanel = new StackPanel();
                rseSkillTypeModifiers = new List<RseSkillTypeModifier>();
                skillTypeModButtonGrid = new Grid();
                addSkillTypeModButton = new Button();
                removeSkillTypeModButton = new Button();
                separator2 = new Separator();
                skillModLabel = new Label();
                skillModPanel = new StackPanel();
                rseSkillModifiers = new List<RseSkillModifier>();
                skillModButtonGrid = new Grid();
                addSkillModButton = new Button();
                removeSkillModButton = new Button();
                separator3 = new Separator();
                customFieldModLabel = new Label();
                customFieldModPanel = new StackPanel();
                rseCustomFieldModifiers = new List<RseCustomFieldModifier>();
                customFieldModButtonGrid = new Grid();
                addCustomFieldModButton = new Button();
                removeCustomFieldModButton = new Button();
                separator4 = new Separator();

                //Add relevent elements as children to parent Panel
                parent.Children.Add(disCharNum);
                parent.Children.Add(disCharPanel);
                parent.Children.Add(separator4);

                //Define index Label
                disCharNum.Content = count + ":";
                disCharNum.Height = 22;
                disCharNum.HorizontalAlignment = HorizontalAlignment.Left;
                disCharNum.Padding = new Thickness(0);
                disCharNum.VerticalContentAlignment = VerticalAlignment.Center;

                //Define main StackPanel and relevent children
                disCharPanel.Margin = new Thickness(40, 0, 0, 0);
                disCharPanel.Children.Add(separator1);
                disCharPanel.Children.Add(skillTypeModLabel);
                disCharPanel.Children.Add(skillTypeModPanel);
                disCharPanel.Children.Add(skillTypeModButtonGrid);
                disCharPanel.Children.Add(separator2);
                disCharPanel.Children.Add(skillModLabel);
                disCharPanel.Children.Add(skillModPanel);
                disCharPanel.Children.Add(skillModButtonGrid);
                disCharPanel.Children.Add(separator3);
                disCharPanel.Children.Add(customFieldModLabel);
                disCharPanel.Children.Add(customFieldModPanel);
                disCharPanel.Children.Add(customFieldModButtonGrid);

                //Update textbox layouts
                name.FixLayout();

                spVal.FixLayout();

                //Define Labels
                skillTypeModLabel.Content = "Skill Type Modifiers:";
                skillTypeModLabel.Height = 22;
                skillTypeModLabel.HorizontalAlignment = HorizontalAlignment.Left;
                skillTypeModLabel.Padding = new Thickness(0);
                skillTypeModLabel.VerticalContentAlignment = VerticalAlignment.Center;

                skillModLabel.Content = "Skill Modifiers:";
                skillModLabel.Height = 22;
                skillModLabel.HorizontalAlignment = HorizontalAlignment.Left;
                skillModLabel.Padding = new Thickness(0);
                skillModLabel.VerticalContentAlignment = VerticalAlignment.Center;

                customFieldModLabel.Content = "Custom Field Modifiers:";
                customFieldModLabel.Height = 22;
                customFieldModLabel.HorizontalAlignment = HorizontalAlignment.Left;
                customFieldModLabel.Padding = new Thickness(0);
                customFieldModLabel.VerticalContentAlignment = VerticalAlignment.Center;

                //Define list StackPanels
                skillTypeModPanel.Margin = new Thickness(40, 0, 0, 0);

                skillModPanel.Margin = new Thickness(40, 0, 0, 0);

                customFieldModPanel.Margin = new Thickness(40, 0, 0, 0);

                //Define list Buttons

                //SkillTypeMod
                skillTypeModButtonGrid.Height = 22;
                skillTypeModButtonGrid.ColumnDefinitions.Add(new ColumnDefinition());
                skillTypeModButtonGrid.ColumnDefinitions.Add(new ColumnDefinition());
                skillTypeModButtonGrid.Children.Add(addSkillTypeModButton);
                skillTypeModButtonGrid.Children.Add(removeSkillTypeModButton);

                addSkillTypeModButton.Width = 75;
                addSkillTypeModButton.Content = "Add New";
                addSkillTypeModButton.Click += btnAddSkillTypeMod_Click;
                Grid.SetColumn(addSkillTypeModButton, 0);

                removeSkillTypeModButton.Width = 75;
                removeSkillTypeModButton.Content = "Remove";
                removeSkillTypeModButton.Click += btnRemoveSkillTypeMod_Click;
                Grid.SetColumn(removeSkillTypeModButton, 1);

                //SkillMod
                skillModButtonGrid.Height = 22;
                skillModButtonGrid.ColumnDefinitions.Add(new ColumnDefinition());
                skillModButtonGrid.ColumnDefinitions.Add(new ColumnDefinition());
                skillModButtonGrid.Children.Add(addSkillModButton);
                skillModButtonGrid.Children.Add(removeSkillModButton);

                addSkillModButton.Width = 75;
                addSkillModButton.Content = "Add New";
                addSkillModButton.Click += btnAddSkillMod_Click;
                Grid.SetColumn(addSkillModButton, 0);

                removeSkillModButton.Width = 75;
                removeSkillModButton.Content = "Remove";
                removeSkillModButton.Click += btnRemoveSkillMod_Click;
                Grid.SetColumn(removeSkillModButton, 1);

                //CustomFieldMod
                customFieldModButtonGrid.Height = 22;
                customFieldModButtonGrid.ColumnDefinitions.Add(new ColumnDefinition());
                customFieldModButtonGrid.ColumnDefinitions.Add(new ColumnDefinition());
                customFieldModButtonGrid.Children.Add(addCustomFieldModButton);
                customFieldModButtonGrid.Children.Add(removeCustomFieldModButton);

                addCustomFieldModButton.Width = 75;
                addCustomFieldModButton.Content = "Add New";
                addCustomFieldModButton.Click += btnAddCustomFieldMod_Click;
                Grid.SetColumn(addCustomFieldModButton, 0);

                removeCustomFieldModButton.Width = 75;
                removeCustomFieldModButton.Content = "Remove";
                removeCustomFieldModButton.Click += btnRemoveCustomFieldMod_Click;
                Grid.SetColumn(removeCustomFieldModButton, 1);
            }

            //--------------------------------------------
            //Functions
            //--------------------------------------------

            /// <summary>
            /// Removes all parent/child connections within and to this object.
            /// </summary>
            public void Remove()
            {
                parent.Children.Remove(disCharNum);
                parent.Children.Remove(disCharPanel);
                parent.Children.Remove(separator4);
            }

            //--------------------------------------------
            //Button Functions
            //--------------------------------------------

            //SkillTypeMod
            private void btnAddSkillTypeMod_Click(object sender, RoutedEventArgs e)
            {
                rseSkillTypeModifiers.Add(new RseSkillTypeModifier(skillTypeModPanel, rseSkillTypeModifiers.Count));
            }

            private void btnRemoveSkillTypeMod_Click(object sender, RoutedEventArgs e)
            {
                rseSkillTypeModifiers[rseSkillTypeModifiers.Count - 1].Remove();
                rseSkillTypeModifiers.RemoveAt(rseSkillTypeModifiers.Count - 1);
            }

            //SkillMod
            private void btnAddSkillMod_Click(object sender, RoutedEventArgs e)
            {
                rseSkillModifiers.Add(new RseSkillModifier(skillModPanel, rseSkillModifiers.Count));
            }

            private void btnRemoveSkillMod_Click(object sender, RoutedEventArgs e)
            {
                rseSkillModifiers[rseSkillModifiers.Count - 1].Remove();
                rseSkillModifiers.RemoveAt(rseSkillModifiers.Count - 1);
            }

            //CustomFieldMod
            private void btnAddCustomFieldMod_Click(object sender, RoutedEventArgs e)
            {
                rseCustomFieldModifiers.Add(new RseCustomFieldModifier(customFieldModPanel, rseCustomFieldModifiers.Count));
            }

            private void btnRemoveCustomFieldMod_Click(object sender, RoutedEventArgs e)
            {
                rseCustomFieldModifiers[rseCustomFieldModifiers.Count - 1].Remove();
                rseCustomFieldModifiers.RemoveAt(rseCustomFieldModifiers.Count - 1);
            }

            //--------------------------------------------
            //Properties
            //--------------------------------------------

            /// <summary>
            /// Gets or sets the contents of the Name TextBox.
            /// </summary>
            public string Name
            {
                get { return name.Text; }
                set { name.Text = value; }
            }

            /// <summary>
            /// Gets or sets the contents of the Default Value TextBox.
            /// </summary>
            public string SkillPointValue
            {
                get { return spVal.Text; }
                set { spVal.Text = value; }
            }

            /// <summary>
            /// Gets or sets the rseSkillTypeModifiers List.
            /// </summary>
            public List<RseSkillTypeModifier> SkillTypeModifiers
            {
                get { return rseSkillTypeModifiers; }
                set { rseSkillTypeModifiers = value; }
            }

            /// <summary>
            /// Gets or sets the rseSkillModifiers List.
            /// </summary>
            public List<RseSkillModifier> SkillModifiers
            {
                get { return rseSkillModifiers; }
                set { rseSkillModifiers = value; }
            }

            /// <summary>
            /// Gets or sets the rseCustomFieldModifiers List.
            /// </summary>
            public List<RseCustomFieldModifier> CustomFieldModifiers
            {
                get { return rseCustomFieldModifiers; }
                set { rseCustomFieldModifiers = value; }
            }
        }
        #endregion

        //--------------------------------------------
        //Variables
        //--------------------------------------------

        List<RseSkillType> rseSkillTypes;
        List<LabeledTextBox> rseVocations;
        List<LabeledTextBox> rseProficiencies;
        List<RseCustomField> rseCustomFields;
        List<RseDisablingCharacteristic> rseDisablingCharacteristics;

        //--------------------------------------------
        //Constructors
        //--------------------------------------------

        public RuleSetEditor()
        {
            InitializeComponent();
            rseSkillTypes = new List<RseSkillType>();
            rseVocations = new List<LabeledTextBox>();
            rseProficiencies = new List<LabeledTextBox>();
            rseCustomFields = new List<RseCustomField>();
            rseDisablingCharacteristics = new List<RseDisablingCharacteristic>();
        }

        //--------------------------------------------
        //File Functions
        //--------------------------------------------

        void save_Click(object sender, RoutedEventArgs e)
        {
            Helpers.Ruleset ruleset = new Helpers.Ruleset();

            if (((TextBox)this.FindName("tbName")).Text.Length > 0)
            {
                ruleset.name = ((TextBox)this.FindName("tbName")).Text;
            }
            else
            {
                return;
            }

            if (((TextBox)this.FindName("tbAttPoints")).Text.Length > 0)
            {
                if (!int.TryParse(((TextBox)this.FindName("tbAttPoints")).Text, out ruleset.attPoints))
                {
                    return;
                }
            }
            else
            {
                return;
            }

            if (((TextBox)this.FindName("tbSkillPoints")).Text.Length > 0)
            {
                if (!int.TryParse(((TextBox)this.FindName("tbSkillPoints")).Text, out ruleset.skillPoints))
                {
                    return;
                }
            }
            else
            {
                return;
            }

            if (((TextBox)this.FindName("tbSPInt")).Text.Length > 0)
            {
                if (!int.TryParse(((TextBox)this.FindName("tbSPInt")).Text, out ruleset.spIntel))
                {
                    return;
                }
            }
            else
            {
                return;
            }

            if (((TextBox)this.FindName("tbDestPoints")).Text.Length > 0)
            {
                if (!int.TryParse(((TextBox)this.FindName("tbDestPoints")).Text, out ruleset.destPoints))
                {
                    return;
                }
            }
            else
            {
                return;
            }

            List<Helpers.Ruleset.SkillType> tempSTList = new List<Helpers.Ruleset.SkillType>();
            for (int i = 0; i < rseSkillTypes.Count; i++)
            {
                Helpers.Ruleset.SkillType tempSkillType = new Helpers.Ruleset.SkillType();

                if (rseSkillTypes[i].Name.Length > 0)
                {
                    tempSkillType.name = rseSkillTypes[i].Name;
                }
                else
                {
                    return;
                }

                tempSkillType.strength = rseSkillTypes[i].Strength;
                tempSkillType.reflex = rseSkillTypes[i].Reflex;
                tempSkillType.intelligence = rseSkillTypes[i].Intelligence;

                List<string> tempSkillList = new List<string>();
                for (int j = 0; j < rseSkillTypes[i].Skills.Count; j++)
                {
                    if (rseSkillTypes[i].Skills[j].Text.Length > 0)
                    {
                        tempSkillList.Add(rseSkillTypes[i].Skills[j].Text);
                    }
                    else
                    {
                        return;
                    }
                }
                tempSkillType.skills = tempSkillList;

                tempSTList.Add(tempSkillType);
            }
            ruleset.skillTypes = tempSTList;

            List<string> tempVocList = new List<string>();
            for (int i = 0; i < rseVocations.Count; i++)
            {
                if (rseVocations[i].Text.Length > 0)
                {
                    tempVocList.Add(rseVocations[i].Text);
                }
                else
                {
                    return;
                }
            }
            ruleset.vocations = tempVocList;

            List<string> tempProfList = new List<string>();
            for (int i = 0; i < rseProficiencies.Count; i++)
            {
                if (rseProficiencies[i].Text.Length > 0)
                {
                    tempProfList.Add(rseProficiencies[i].Text);
                }
                else
                {
                    return;
                }
            }
            ruleset.proficiencies = tempProfList;

            List<Helpers.Ruleset.CustomField> tempCFList = new List<Helpers.Ruleset.CustomField>();
            for (int i = 0; i < rseCustomFields.Count; i++)
            {
                Helpers.Ruleset.CustomField tempCustField = new Helpers.Ruleset.CustomField();

                if (rseCustomFields[i].Name.Length > 0)
                {
                    tempCustField.name = rseCustomFields[i].Name;
                }
                else
                {
                    return;
                }

                if (rseCustomFields[i].DefaultValue.Length > 0)
                {
                    if (!int.TryParse(rseCustomFields[i].DefaultValue, out tempCustField.defaultVal))
                    {
                        return;
                    }
                }
                else
                {
                    return;
                }

                tempCustField.fieldIsOptional = rseCustomFields[i].IsOptional;

                tempCFList.Add(tempCustField);
            }
            ruleset.customFields = tempCFList;

            List<Helpers.Ruleset.DisablingCharacteristic> tempDCList = new List<Helpers.Ruleset.DisablingCharacteristic>();
            for (int i = 0; i < rseDisablingCharacteristics.Count; i++)
            {
                Helpers.Ruleset.DisablingCharacteristic tempDisChar = new Helpers.Ruleset.DisablingCharacteristic();

                if (rseDisablingCharacteristics[i].Name.Length > 0)
                {
                    tempDisChar.name = rseDisablingCharacteristics[i].Name;
                }
                else
                {
                    return;
                }

                if (rseDisablingCharacteristics[i].SkillPointValue.Length > 0)
                {
                    if (!int.TryParse(rseDisablingCharacteristics[i].SkillPointValue, out tempDisChar.spVal))
                    {
                        return;
                    }
                }
                else
                {
                    return;
                }

                List<Helpers.Ruleset.DisablingCharacteristic.SkillTypeModifier> tempTypeModList = new List<Helpers.Ruleset.DisablingCharacteristic.SkillTypeModifier>();
                for (int j = 0; j < rseDisablingCharacteristics[i].SkillTypeModifiers.Count; j++)
                {
                    Helpers.Ruleset.DisablingCharacteristic.SkillTypeModifier tempTypeMod = new Helpers.Ruleset.DisablingCharacteristic.SkillTypeModifier();

                    if (rseDisablingCharacteristics[i].SkillTypeModifiers[j].SkillTypeIndex.Length > 0)
                    {
                        if (!int.TryParse(rseDisablingCharacteristics[i].SkillTypeModifiers[j].SkillTypeIndex, out tempTypeMod.skillTypeIndex))
                        {
                            return;
                        }
                    }
                    else
                    {
                        return;
                    }

                    if (rseDisablingCharacteristics[i].SkillTypeModifiers[j].DiceReduction.Length > 0)
                    {
                        if (!int.TryParse(rseDisablingCharacteristics[i].SkillTypeModifiers[j].DiceReduction, out tempTypeMod.diceReduction))
                        {
                            return;
                        }
                    }
                    else
                    {
                        return;
                    }

                    if (rseDisablingCharacteristics[i].SkillTypeModifiers[j].SkillPointCostMultiplier.Length > 0)
                    {
                        if (!int.TryParse(rseDisablingCharacteristics[i].SkillTypeModifiers[j].SkillPointCostMultiplier, out tempTypeMod.spCostMult))
                        {
                            return;
                        }
                    }
                    else
                    {
                        return;
                    }

                    tempTypeModList.Add(tempTypeMod);
                }
                tempDisChar.skillTypeModifiers = tempTypeModList;

                List<Helpers.Ruleset.DisablingCharacteristic.SkillModifier> tempSkillModList = new List<Helpers.Ruleset.DisablingCharacteristic.SkillModifier>();
                for (int j = 0; j < rseDisablingCharacteristics[i].SkillModifiers.Count; j++)
                {
                    Helpers.Ruleset.DisablingCharacteristic.SkillModifier tempSkillMod = new Helpers.Ruleset.DisablingCharacteristic.SkillModifier();

                    if (rseDisablingCharacteristics[i].SkillModifiers[j].SkillTypeIndex.Length > 0)
                    {
                        if (!int.TryParse(rseDisablingCharacteristics[i].SkillModifiers[j].SkillTypeIndex, out tempSkillMod.skillTypeIndex))
                        {
                            return;
                        }
                    }
                    else
                    {
                        return;
                    }

                    if (rseDisablingCharacteristics[i].SkillModifiers[j].SkillIndex.Length > 0)
                    {
                        if (!int.TryParse(rseDisablingCharacteristics[i].SkillModifiers[j].SkillIndex, out tempSkillMod.skillIndex))
                        {
                            return;
                        }
                    }
                    else
                    {
                        return;
                    }

                    if (rseDisablingCharacteristics[i].SkillModifiers[j].DiceReduction.Length > 0)
                    {
                        if (!int.TryParse(rseDisablingCharacteristics[i].SkillModifiers[j].DiceReduction, out tempSkillMod.diceReduction))
                        {
                            return;
                        }
                    }
                    else
                    {
                        return;
                    }

                    if (rseDisablingCharacteristics[i].SkillModifiers[j].SkillPointCostMultiplier.Length > 0)
                    {
                        if (!int.TryParse(rseDisablingCharacteristics[i].SkillModifiers[j].SkillPointCostMultiplier, out tempSkillMod.spCostMult))
                        {
                            return;
                        }
                    }
                    else
                    {
                        return;
                    }

                    tempSkillModList.Add(tempSkillMod);
                }
                tempDisChar.skillModifiers = tempSkillModList;

                List<Helpers.Ruleset.DisablingCharacteristic.CustomFieldModifier> tempCFModList = new List<Helpers.Ruleset.DisablingCharacteristic.CustomFieldModifier>();
                for(int j = 0; j < rseDisablingCharacteristics[i].CustomFieldModifiers.Count; j++)
                {
                    Helpers.Ruleset.DisablingCharacteristic.CustomFieldModifier tempCustFieldMod = new Helpers.Ruleset.DisablingCharacteristic.CustomFieldModifier();
                    
                    if (rseDisablingCharacteristics[i].CustomFieldModifiers[j].CustomFieldIndex.Length > 0)
                    {
                        if (!int.TryParse(rseDisablingCharacteristics[i].CustomFieldModifiers[j].CustomFieldIndex, out tempCustFieldMod.fieldIndex))
                        {
                            return;
                        }
                    }
                    else
                    {
                        return;
                    }

                    if (rseDisablingCharacteristics[i].CustomFieldModifiers[j].ValueModifier.Length > 0)
                    {
                        if (!int.TryParse(rseDisablingCharacteristics[i].CustomFieldModifiers[j].ValueModifier, out tempCustFieldMod.valMod))
                        {
                            return;
                        }
                    }
                    else
                    {
                        return;
                    }

                    tempCustFieldMod.disableField = rseDisablingCharacteristics[i].CustomFieldModifiers[j].DisableField;

                    tempCFModList.Add(tempCustFieldMod);
                }
                tempDisChar.customFieldModifiers = tempCFModList;

                tempDCList.Add(tempDisChar);
            }
            ruleset.disablingCharacteristics = tempDCList;

            string output = JsonConvert.SerializeObject(ruleset);
        }

        //--------------------------------------------
        //Button Functions
        //--------------------------------------------

        //Skill Type Buttons
        void btnAddNewST_Click(object sender, RoutedEventArgs e)
        {
            rseSkillTypes.Add(new RseSkillType((StackPanel)this.FindName("stkSkillTypes"), rseSkillTypes.Count));
        }

        void btnRemoveST_Click(object sender, RoutedEventArgs e)
        {
            if (rseSkillTypes.Count > 0)
            {
                rseSkillTypes[rseSkillTypes.Count - 1].Remove();
                rseSkillTypes.RemoveAt(rseSkillTypes.Count - 1);
            }
        }

        //Vocation Buttons
        void btnAddNewVoc_Click(object sender, RoutedEventArgs e)
        {
            rseVocations.Add(new LabeledTextBox((StackPanel)this.FindName("stkVocations"), rseVocations.Count + ":"));
        }

        void btnRemoveVoc_Click(object sender, RoutedEventArgs e)
        {
            if (rseVocations.Count > 0)
            {
                rseVocations[rseVocations.Count - 1].Remove();
                rseVocations.RemoveAt(rseVocations.Count - 1);
            }
        }

        //Proficiency Buttons
        void btnAddNewProf_Click(object sender, RoutedEventArgs e)
        {
            rseProficiencies.Add(new LabeledTextBox((StackPanel)this.FindName("stkProficiencies"), rseProficiencies.Count + ":"));
        }

        void btnRemoveProf_Click(object sender, RoutedEventArgs e)
        {
            if (rseProficiencies.Count > 0)
            {
                rseProficiencies[rseProficiencies.Count - 1].Remove();
                rseProficiencies.RemoveAt(rseProficiencies.Count - 1);
            }
        }

        //Custom Field Buttons
        void btnAddNewCustomField_Click(object sender, RoutedEventArgs e)
        {
            rseCustomFields.Add(new RseCustomField((StackPanel)this.FindName("stkCustomFields"), rseCustomFields.Count));
        }

        void btnRemoveCustomField_Click(object sender, RoutedEventArgs e)
        {
            if (rseCustomFields.Count > 0)
            {
                rseCustomFields[rseCustomFields.Count - 1].Remove();
                rseCustomFields.RemoveAt(rseCustomFields.Count - 1);
            }
        }

        //Disabling Characteristic Buttons
        void btnAddNewDisablingCharacteristic_Click(object sender, RoutedEventArgs e)
        {
            rseDisablingCharacteristics.Add(new RseDisablingCharacteristic((StackPanel)this.FindName("stkDisablingCharacteristics"), rseDisablingCharacteristics.Count));
        }

        void btnRemoveDisablingCharacteristic_Click(object sender, RoutedEventArgs e)
        {
            if (rseDisablingCharacteristics.Count > 0)
            {
                rseDisablingCharacteristics[rseDisablingCharacteristics.Count - 1].Remove();
                rseDisablingCharacteristics.RemoveAt(rseDisablingCharacteristics.Count - 1);
            }
        }
    }
}
