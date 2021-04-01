using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InputCommandManager : MonoBehaviour
{
    private static InputCommandManager _instance;
    
    public static InputCommandManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("The Input Command Manager is NULL!!");
            }
            
            return _instance;
        }
    }
    
    private List<ICommand> _commandBuffer = new List<ICommand>();
    public bool IsCoroutineRunning = false;

    private void Awake()
    {
        _instance = this;
    }
    
        public void AddCommandToBuffer(ICommand commandToAdd)
        {
            _commandBuffer.Add(commandToAdd);
        }

        public void ReplayCommandBuffer()
        {
            StartCoroutine(Replay());
        }
            IEnumerator Replay()
            {
                IsCoroutineRunning = true;
                Debug.Log("Started Coroutine: " + Replay());
                foreach (var command in _commandBuffer)
                {
                    command.Execute();
                    yield return new WaitForEndOfFrame();
                }
                Debug.Log("Finished Coroutine: " + Replay());
                IsCoroutineRunning = false;
            }
        
        public void RewindCommandBuffer()
        {
            StartCoroutine(Rewind());
        }
            IEnumerator Rewind()
            {
                IsCoroutineRunning = true;
                Debug.Log("Started Coroutine: " + Rewind());
                foreach (var command in Enumerable.Reverse(_commandBuffer))
                {
                    command.Undo();
                    yield return new WaitForEndOfFrame();
                }
                Debug.Log("Finished Coroutine: " + Rewind());
                IsCoroutineRunning = false;
            }
}
