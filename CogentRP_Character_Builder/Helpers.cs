using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CogentRP_Character_Builder
{
    static class Helpers
    {
        struct Ruleset
        {
            private struct SkillType
            {
                private struct ActiveAttributes
                {
                    bool strength;
                    bool reflex;
                    bool intelligence;
                }
                string name;
                ActiveAttributes activeAttributes;
                string[] skills;
            }

            struct CustomField
            {
                string name;
                int defaultVal;
                bool fieldIsOptional;
            }

            struct DisablingCharacteristic
            {
                private struct SkillTypeModifier
                {
                    int skillTypeIndex;
                    int diceReduction;
                    int spCostMult;
                }
                private struct SkillModifier
                {
                    int skillTypeIndex;
                    int skillIndex;
                    int diceReduction;
                    int spCostMult;
                }
                private struct CustomFieldModifier
                {
                    int fieldIndex;
                    int valMod;
                    int spCostMult;
                    bool disableField;
                }
                
                string name;
                int spVal;
                SkillTypeModifier[] skillTypeModifiers;
                SkillModifier[] skillModifiers;
                CustomFieldModifier[] customFieldModifiers;
            }

            string name;
            int attPoints;
            int skillPoints;
            int spIntel;
            int destPoints;
            SkillType[] skillTypes;
            string[] vocations;
            string[] proficiencies;
            CustomField[] customFields;
            DisablingCharacteristic[] disablingCharacteristics;
        }
    }
}
