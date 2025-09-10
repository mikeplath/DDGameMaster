using System;

namespace DDGameMaster.Models.Game
{
    public class ActionManager
    {
        public event EventHandler<ActionEventArgs>? ActionPerformed;
        
        public void PerformDoAction(string description)
        {
            var action = new GameAction
            {
                Type = ActionType.Do,
                Description = description,
                Timestamp = DateTime.Now
            };
            
            OnActionPerformed(action);
        }
        
        public void PerformSayAction(string dialogue)
        {
            var action = new GameAction
            {
                Type = ActionType.Say,
                Description = dialogue,
                Timestamp = DateTime.Now
            };
            
            OnActionPerformed(action);
        }
        
        public void PerformStoryAction(string narrative)
        {
            var action = new GameAction
            {
                Type = ActionType.Story,
                Description = narrative,
                Timestamp = DateTime.Now
            };
            
            OnActionPerformed(action);
        }
        
        public void PerformSeeAction()
        {
            var action = new GameAction
            {
                Type = ActionType.See,
                Description = "Generate scene image",
                Timestamp = DateTime.Now
            };
            
            OnActionPerformed(action);
        }
        
        protected virtual void OnActionPerformed(GameAction action)
        {
            ActionPerformed?.Invoke(this, new ActionEventArgs(action));
        }
    }
    
    public enum ActionType
    {
        Do,
        Say,
        Story,
        See
    }
    
    public class GameAction
    {
        public ActionType Type { get; set; }
        public string Description { get; set; } = "";
        public DateTime Timestamp { get; set; }
    }
    
    public class ActionEventArgs : EventArgs
    {
        public GameAction Action { get; }
        
        public ActionEventArgs(GameAction action)
        {
            Action = action;
        }
    }
}