using ECS.System.SystemCalbacks;
using UnityEditor;
using UnityEngine;

namespace ECS.CodeBase.CustomEditors
{
    [CustomEditor(typeof(Test))]
    internal class TestCustomEditor : Editor
    {
        private Test test;
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            //test = (Test)target;

            //if (GUILayout.Button("SetValue"))
            //{ 
            //    if (test.movementSystem is IFixedUpdateSystem @if)
            //    {
            //        @if.t = test.value;
            //    } 
            //    test.movementSystem.r = test.value.y;

            //    SaveChanges();
            //}

            //if (GUILayout.Button("GetValue"))
            //{
            //    if (test.movementSystem is IFixedUpdateSystem @if)
            //    {
            //        Debug.Log($"Vector: {@if.t} float: {test.movementSystem.r}");
            //    }
            //}
        }
    }
}
