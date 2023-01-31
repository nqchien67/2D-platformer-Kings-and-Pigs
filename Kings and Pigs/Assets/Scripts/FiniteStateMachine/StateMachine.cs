using UnityEngine;

namespace FiniteStateMachine
{
    public class StateMachine : MonoBehaviour
    {
        private BaseState currentState;

        private void Start()
        {
            currentState = GetInitialState();
            if (currentState != null)
                currentState.Enter();
        }

        protected virtual void Update()
        {
            if (currentState != null)
                currentState.Update();
        }

        private void FixedUpdate()
        {
            if (currentState != null)
                currentState.FixedUpdate();
        }

        public void ChangeState(BaseState newState)
        {
            currentState.Exit();

            currentState = newState;
            newState.Enter();
        }

        protected virtual BaseState GetInitialState()
        {
            return null;
        }

        private void OnGUI()
        {
            string content = currentState != null ? currentState.name : "(no current state)";
            GUILayout.Label($"<color='black'><size=40>{content}</size></color>");
        }
    }
}
