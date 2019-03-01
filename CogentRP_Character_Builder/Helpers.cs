using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CogentRP_Character_Builder
{
    static class Helpers
    {
        public struct Ruleset
        {
            public struct SkillType
            {
                public string name;
                public bool strength;
                public bool reflex;
                public bool intelligence;
                public List<string> skills;
            }

            public struct CustomField
            {
                public string name;
                public int defaultVal;
                public bool fieldIsOptional;
            }

            public struct DisablingCharacteristic
            {
                public struct SkillTypeModifier
                {
                    public int skillTypeIndex;
                    public int diceReduction;
                    public int spCostMult;
                }
                public struct SkillModifier
                {
                    public int skillTypeIndex;
                    public int skillIndex;
                    public int diceReduction;
                    public int spCostMult;
                }
                public struct CustomFieldModifier
                {
                    public int fieldIndex;
                    public int valMod;
                    public bool disableField;
                }

                public string name;
                public int spVal;
                public List<SkillTypeModifier> skillTypeModifiers;
                public List<SkillModifier> skillModifiers;
                public List<CustomFieldModifier> customFieldModifiers;
            }

            public string name;
            public int attPoints;
            public int skillPoints;
            public int spIntel;
            public int destPoints;
            public List<SkillType> skillTypes;
            public List<string> vocations;
            public List<string> proficiencies;
            public List<CustomField> customFields;
            public List<DisablingCharacteristic> disablingCharacteristics;
        }

        public struct FileInfo
        {
            string fileFame;
            string editorName;
        }

        public struct FileTracker
        {
            List<FileInfo> ruleFiles;
            List<FileInfo> characterFiles;
        }
    }
}
