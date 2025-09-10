using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using DDGameMaster.Models.Character;

namespace DDGameMaster.Views
{
    public partial class CharacterCreationView : Window
    {
        private Dictionary<string, int> baseAbilityScores = new Dictionary<string, int>
        {
            { "Strength", 8 },
            { "Dexterity", 8 },
            { "Constitution", 8 },
            { "Intelligence", 8 },
            { "Wisdom", 8 },
            { "Charisma", 8 }
        };
        
        private Dictionary<string, Button> minusButtons = new();
        private Dictionary<string, Button> plusButtons = new();
        private Dictionary<string, TextBlock> scoreLabels = new();
        private Dictionary<string, TextBlock> finalScoreLabels = new();
        
        private int pointsRemaining = 27;
        private CharacterRace? selectedRace;
        private CharacterClass? selectedClass;
        
        private List<CharacterRace> availableRaces = CharacterRace.GetDefaultRaces();
        private List<CharacterClass> availableClasses = CharacterClass.GetDefaultClasses();
        
        public CharacterStats? CreatedCharacter { get; private set; }
        
        public CharacterCreationView()
        {
            InitializeComponent();
            InitializeData();
            CreateAbilityScoreControls();
            CreateFinalStatsDisplay();
            UpdateAll();
        }
        
        private void InitializeData()
        {
            RaceListBox.ItemsSource = availableRaces;
            ClassListBox.ItemsSource = availableClasses;
        }
        
        private void CreateAbilityScoreControls()
        {
            foreach (var ability in baseAbilityScores.Keys)
            {
                var panel = new StackPanel { Orientation = Orientation.Horizontal, Margin = new Thickness(0, 5, 0, 5) };
                
                // Ability name label
                var nameLabel = new TextBlock 
                { 
                    Text = ability + ":", 
                    Width = 80, 
                    Foreground = System.Windows.Media.Brushes.White,
                    VerticalAlignment = VerticalAlignment.Center
                };
                panel.Children.Add(nameLabel);
                
                // Minus button
                var minusBtn = new Button 
                { 
                    Content = "-", 
                    Width = 30, 
                    Height = 25,
                    Background = System.Windows.Media.Brushes.Red,
                    Foreground = System.Windows.Media.Brushes.White,
                    BorderThickness = new Thickness(0),
                    Margin = new Thickness(5, 0, 5, 0)
                };
                minusBtn.Click += (s, e) => ModifyAbilityScore(ability, -1);
                minusButtons[ability] = minusBtn;
                panel.Children.Add(minusBtn);
                
                // Score label
                var scoreLabel = new TextBlock 
                { 
                    Text = "8", 
                    Width = 30, 
                    TextAlignment = TextAlignment.Center,
                    Foreground = System.Windows.Media.Brushes.White,
                    VerticalAlignment = VerticalAlignment.Center,
                    FontWeight = FontWeights.Bold
                };
                scoreLabels[ability] = scoreLabel;
                panel.Children.Add(scoreLabel);
                
                // Plus button
                var plusBtn = new Button 
                { 
                    Content = "+", 
                    Width = 30, 
                    Height = 25,
                    Background = System.Windows.Media.Brushes.Green,
                    Foreground = System.Windows.Media.Brushes.White,
                    BorderThickness = new Thickness(0),
                    Margin = new Thickness(5, 0, 5, 0)
                };
                plusBtn.Click += (s, e) => ModifyAbilityScore(ability, 1);
                plusButtons[ability] = plusBtn;
                panel.Children.Add(plusBtn);
                
                // Cost display
                var costLabel = new TextBlock 
                { 
                    Text = GetPointCost(8).ToString(), 
                    Width = 40, 
                    TextAlignment = TextAlignment.Center,
                    Foreground = System.Windows.Media.Brushes.Yellow,
                    VerticalAlignment = VerticalAlignment.Center,
                    Margin = new Thickness(10, 0, 0, 0)
                };
                panel.Children.Add(costLabel);
                panel.Children.Add(new TextBlock 
                { 
                    Text = "pts", 
                    Foreground = System.Windows.Media.Brushes.Gray,
                    VerticalAlignment = VerticalAlignment.Center,
                    Margin = new Thickness(2, 0, 0, 0)
                });
                
                AbilityScoresPanel.Children.Add(panel);
            }
        }
        
        private void CreateFinalStatsDisplay()
        {
            foreach (var ability in baseAbilityScores.Keys)
            {
                var panel = new StackPanel { Orientation = Orientation.Horizontal, Margin = new Thickness(0, 2, 0, 2) };
                
                var nameLabel = new TextBlock 
                { 
                    Text = ability.Substring(0, 3).ToUpper() + ":", 
                    Width = 40, 
                    Foreground = System.Windows.Media.Brushes.White,
                    FontSize = 12
                };
                panel.Children.Add(nameLabel);
                
                var finalScore = new TextBlock 
                { 
                    Text = "8 (+0)", 
                    Foreground = System.Windows.Media.Brushes.Yellow,
                    FontWeight = FontWeights.Bold,
                    FontSize = 12
                };
                finalScoreLabels[ability] = finalScore;
                panel.Children.Add(finalScore);
                
                FinalStatsPanel.Children.Add(panel);
            }
        }
        
        private void ModifyAbilityScore(string ability, int change)
        {
            int currentScore = baseAbilityScores[ability];
            int newScore = currentScore + change;
            
            // Validate bounds
            if (newScore < 8 || newScore > 15) return;
            
            // Calculate point cost difference
            int currentCost = GetPointCost(currentScore);
            int newCost = GetPointCost(newScore);
            int costDifference = newCost - currentCost;
            
            // Check if we have enough points
            if (costDifference > pointsRemaining) return;
            
            // Apply the change
            baseAbilityScores[ability] = newScore;
            pointsRemaining -= costDifference;
            
            UpdateAll();
        }
        
        private int GetPointCost(int score)
        {
            return score switch
            {
                8 => 0,
                9 => 1,
                10 => 2,
                11 => 3,
                12 => 4,
                13 => 5,
                14 => 7,
                15 => 9,
                _ => 0
            };
        }
        
        private void UpdateAll()
        {
            UpdatePointsDisplay();
            UpdateAbilityScoreButtons();
            UpdateFinalStats();
            UpdateCharacterSummary();
            UpdateCreateButton();
        }
        
        private void UpdatePointsDisplay()
        {
            PointsRemainingText.Text = $"Points Remaining: {pointsRemaining}";
        }
        
        private void UpdateAbilityScoreButtons()
        {
            foreach (var ability in baseAbilityScores.Keys)
            {
                int currentScore = baseAbilityScores[ability];
                scoreLabels[ability].Text = currentScore.ToString();
                
                // Update button states
                minusButtons[ability].IsEnabled = currentScore > 8;
                
                int nextScore = currentScore + 1;
                int costForNext = GetPointCost(nextScore) - GetPointCost(currentScore);
                plusButtons[ability].IsEnabled = nextScore <= 15 && costForNext <= pointsRemaining;
            }
        }
        
        private void UpdateFinalStats()
        {
            foreach (var ability in baseAbilityScores.Keys)
            {
                int baseScore = baseAbilityScores[ability];
                int racialBonus = GetRacialBonus(ability);
                int finalScore = baseScore + racialBonus;
                int modifier = (finalScore - 10) / 2;
                
                string modifierText = modifier >= 0 ? $"+{modifier}" : modifier.ToString();
                finalScoreLabels[ability].Text = $"{finalScore} ({modifierText})";
            }
        }
        
        private int GetRacialBonus(string ability)
        {
            if (selectedRace == null) return 0;
            
            // Check for specific ability bonus
            if (selectedRace.AbilityScoreIncrease.ContainsKey(ability))
                return selectedRace.AbilityScoreIncrease[ability];
            
            // Check for "ALL" bonus (like Human)
            if (selectedRace.AbilityScoreIncrease.ContainsKey("ALL"))
                return selectedRace.AbilityScoreIncrease["ALL"];
            
            return 0;
        }
        
        private void UpdateCharacterSummary()
        {
            SummaryNameText.Text = string.IsNullOrWhiteSpace(CharacterNameBox.Text) ? "Unnamed Character" : CharacterNameBox.Text;
            SummaryRaceText.Text = selectedRace?.Name ?? "Race: None Selected";
            SummaryClassText.Text = selectedClass?.Name ?? "Class: None Selected";
        }
        
        private void UpdateCreateButton()
        {
            CreateCharacterButton.IsEnabled = selectedRace != null && selectedClass != null && 
                                             !string.IsNullOrWhiteSpace(CharacterNameBox.Text);
        }
        
        private void RaceListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedRace = RaceListBox.SelectedItem as CharacterRace;
            UpdateAll();
        }
        
        private void ClassListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedClass = ClassListBox.SelectedItem as CharacterClass;
            UpdateAll();
        }
        
        private void UseStandardArray_Click(object sender, RoutedEventArgs e)
        {
            // Standard Array: 15, 14, 13, 12, 10, 8
            var standardScores = new List<int> { 15, 14, 13, 12, 10, 8 };
            var abilities = baseAbilityScores.Keys.ToList();
            
            for (int i = 0; i < abilities.Count && i < standardScores.Count; i++)
            {
                baseAbilityScores[abilities[i]] = standardScores[i];
            }
            
            pointsRemaining = 0; // Standard array uses no points
            UpdateAll();
        }
        
        private void QuickCharacter_Click(object sender, RoutedEventArgs e)
        {
            var random = new Random();
            
            // Random name
            string[] names = { "Aragorn", "Legolas", "Gimli", "Gandalf", "Thorin", "Bilbo", "Frodo", "Sam", "Merry", "Pippin" };
            CharacterNameBox.Text = names[random.Next(names.Length)];
            
            // Random race
            selectedRace = availableRaces[random.Next(availableRaces.Count)];
            RaceListBox.SelectedItem = selectedRace;
            
            // Random class
            selectedClass = availableClasses[random.Next(availableClasses.Count)];
            ClassListBox.SelectedItem = selectedClass;
            
            // Random ability scores (point buy simulation)
            UseStandardArray_Click(sender, e);
            
            // Shuffle the standard array
            var abilities = baseAbilityScores.Keys.ToList();
            var scores = baseAbilityScores.Values.ToList();
            
            for (int i = 0; i < scores.Count; i++)
            {
                int j = random.Next(i, scores.Count);
                (scores[i], scores[j]) = (scores[j], scores[i]);
            }
            
            for (int i = 0; i < abilities.Count; i++)
            {
                baseAbilityScores[abilities[i]] = scores[i];
            }
            
            UpdateAll();
        }
        
        private void CreateCharacter_Click(object sender, RoutedEventArgs e)
        {
            if (selectedRace == null || selectedClass == null) return;
            
            // Calculate final ability scores with racial bonuses
            var finalStats = new CharacterStats
            {
                Name = CharacterNameBox.Text.Trim(),
                Level = 1,
                Strength = baseAbilityScores["Strength"] + GetRacialBonus("Strength"),
                Dexterity = baseAbilityScores["Dexterity"] + GetRacialBonus("Dexterity"),
                Constitution = baseAbilityScores["Constitution"] + GetRacialBonus("Constitution"),
                Intelligence = baseAbilityScores["Intelligence"] + GetRacialBonus("Intelligence"),
                Wisdom = baseAbilityScores["Wisdom"] + GetRacialBonus("Wisdom"),
                Charisma = baseAbilityScores["Charisma"] + GetRacialBonus("Charisma"),
                Speed = selectedRace.Size
            };
            
            // Calculate HP (Class hit die + CON modifier)
            int conModifier = (finalStats.Constitution - 10) / 2;
            finalStats.MaxHP = selectedClass.HitDie + conModifier;
            finalStats.CurrentHP = finalStats.MaxHP;
            
            // Calculate AC (10 + DEX modifier, will be modified by armor later)
            int dexModifier = (finalStats.Dexterity - 10) / 2;
            finalStats.ArmorClass = 10 + dexModifier;
            
            CreatedCharacter = finalStats;
            
            // Store additional character data for later use
            var characterData = new Dictionary<string, object>
            {
                ["Race"] = selectedRace,
                ["Class"] = selectedClass,
                ["Backstory"] = BackstoryBox.Text
            };
            
            DialogResult = true;
            Close();
        }
    }
}