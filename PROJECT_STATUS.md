# AI Handover: D&D Game Master Project Status

## 1. Core Instructions for Assisting the User

**THIS IS THE MOST IMPORTANT SECTION. YOU MUST FOLLOW THESE RULES AT ALL TIMES.**

*   **Assume User Incompetence:** The user, snugg, is to be treated as completely new to coding and incompetent with computers. Do not assume they know anything.
*   **Provide Every Step, Every Time:** For every action, provide explicit, numbered, step-by-step instructions. Never skip steps, no matter how simple or how many times they have been performed before.
*   **Full File Replacement Only:** We do not edit files. When a file needs to be changed, you must provide the commands to **first remove the old file** and **then create a new, empty file** which you will then provide the complete, updated code for.
*   **Use PowerShell for File Operations:** All file operations (`Remove-Item`, `notepad`) must be done using **Windows PowerShell**.
*   **Use Git Bash for Git Commands:** All version control (`git`) commands must be done using **Git Bash**. You must instruct the user to switch to Git Bash when it is time to upload.
*   **Use Exact File Paths:** Always use the full, absolute path for the user's machine: `C:\Users\snugg\Desktop\DnD\...`
*   **Provide Complete Code Blocks:** Every time a file is created, provide the *entire* file's contents in a single code block for the user to copy and paste. Do not use snippets or partial code.

## 2. Project Overview

*   **Project Name:** D&D Game Master
*   **Purpose:** A desktop application for Windows to help a Dungeon Master manage Dungeons & Dragons games.
*   **Technology:** C# with WPF (Windows Presentation Foundation) using .NET.
*   **Development Environment:** Windows 10, using PowerShell for file editing and Git Bash for source control.

## 3. Current Features (What's Been Done)

The application is a single window with a main menu that navigates to different pages (Views).

*   **Character Creation:**
    *   A comprehensive creation screen (`CharacterCreationView.xaml`).
    *   Users can input: Name, Race, Class, Alignment, Ability Scores (Strength, Dexterity, etc.), Appearance, Backstory, Personality Traits, Ideals, Bonds, and Flaws.
    *   Users can select Skill Proficiencies from a list of checkboxes.
    *   Based on Race and Class selection, the character is automatically assigned some starting abilities, saving throw proficiencies, and a Hit Die value.

*   **Character Sheet:**
    *   A detailed view (`CharacterSheetView.xaml`) that displays all created character information.
    *   Calculates and displays saving throw bonuses and skill bonuses (highlighting proficient skills).
    *   Displays HP, AC, Level, and XP.
    *   Allows for applying damage and healing to current HP.
    *   Allows for adding XP, which automatically handles leveling up the character (calculating new HP and showing a notification).

*   **Inventory System:**
    *   The Character Sheet has an inventory section.
    *   Users can add new items with both a **Name** and a **Description**.
    *   Clicking an item in the inventory list opens a pop-up window showing its full description.

*   **Save/Load System:**
    *   Characters can be saved to a `.json` file.
    *   Characters can be loaded from a `.json` file, restoring all their information.

*   **Dice Roller:**
    *   A simple page (`DiceRollerView.xaml`) for rolling dice (d4, d6, d8, d10, d12, d20, d100).
    *   Shows the result of each roll.

*   **DM Notes:**
    *   A dedicated page (`NotesView.xaml`) with a large text box for the DM to take notes.
    *   Notes can be saved to a `.txt` file.
    *   Notes can be loaded from a `.txt` file.

## 4. Project File Structure

*   `C:\Users\snugg\Desktop\DnD\DDGameMaster\` - The root project folder.
*   `\Models\Character\` - Contains the "blueprints" for the character data (`Character.cs`, `Item.cs`, etc.).
*   `\Models\Game\` - Contains the global `GameState.cs` to hold the currently loaded character.
*   `\Views\` - Contains all the XAML pages for the UI (`CharacterCreationView.xaml`, `CharacterSheetView.xaml`, etc.).
*   `MainWindow.xaml` and `MainWindow.xaml.cs` are the main entry point and menu.

## 5. Next Steps (What Needs to Happen)

The user has expressed a desire to continue adding features. Here are the logical next steps:

1.  **Improve the Inventory:** The inventory is functional but basic.
    *   **Remove Item:** Add a "Remove Item" button to the character sheet.
    *   **Edit Item:** Add a way to edit an item's name or description after it's been created.
    *   **Item Stacks:** Allow items to be stacked (e.g., "Arrows x20").
    *   **Equipment Slots:** Create specific slots for equipped items (Weapon, Armor, Shield) that could affect character stats like Armor Class.

2.  **Create a Spellbook:**
    *   Create a `Spell.cs` model with properties like `Name`, `Level`, `School`, `CastingTime`, `Range`, `Components`, `Duration`, `Description`.
    *   Add a `Spellbook` list to the `Character.cs` model.
    *   Create a new `SpellbookView.xaml` to display spells, filterable by level.
    *   Update the character creation/sheet to allow adding/viewing spells.

3.  **Build an Encounter Tracker:**
    *   Create a `Creature.cs` model with basic stats (Name, HP, AC, Initiative).
    *   Create a new `EncounterTrackerView.xaml`.
    *   Allow the DM to add multiple creatures to a list, roll initiative for them, and track their HP during combat.