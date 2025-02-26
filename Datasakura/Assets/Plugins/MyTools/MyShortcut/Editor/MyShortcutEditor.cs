using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace MyShortcut
{
    /// <summary>
    /// Окно тулзы
    /// </summary>
    public class MyShortcutEditor : EditorWindow
    {
        private static EditorWindow window = null;

        private readonly string MyShortcutDataFileName = "MyShortcutData.asset";

        private readonly Color AddFieldColor = new Color(0f, 0.25f, 0f, 0.2f);

        private readonly Color ListFieldColor = new Color(0f, 0f, 0f, 0.2f);

        private MyShortcutData shortcutData = null;
        private Object newObject = null;
        private Vector2 scrollPos = Vector2.zero;


        [MenuItem("Tools/MyShortcut", false, 1)]
        private static void ShowWindow()
        {
            window = GetWindow(typeof(MyShortcutEditor));
            window.titleContent.text = "MyShortcut";
        }

        private void OnEnable()
        {
            CacheShortcutData();
            RemoveEmptyElementsInData();
        }

        private void CacheShortcutData()
        {
            string configFilePath = GetEditorScriptFilePath() + MyShortcutDataFileName;

            shortcutData = (MyShortcutData)(AssetDatabase.LoadAssetAtPath(configFilePath, typeof(MyShortcutData)));

            if (shortcutData == null)
            {
                AssetDatabase.CreateAsset(CreateInstance<MyShortcutData>(), configFilePath);
                AssetDatabase.SaveAssets();

                shortcutData = (MyShortcutData)(AssetDatabase.LoadAssetAtPath(configFilePath, typeof(MyShortcutData)));
            }
        }

        private string GetEditorScriptFilePath()
        {
            MonoScript ms = MonoScript.FromScriptableObject(this);
            string m_ScriptFilePath = AssetDatabase.GetAssetPath(ms);

            return m_ScriptFilePath.Split(new[] { ms.name + ".cs" }, System.StringSplitOptions.None)[0];
        }

        private void RemoveEmptyElementsInData()
        {
            shortcutData.dataInfoList = shortcutData.dataInfoList.Where(x => x.obj != null).Distinct().ToList();
            EditorUtility.SetDirty(shortcutData);
        }

        private List<MyShortcutData.MyShortcutDataInfo> SortObjectsByType(List<MyShortcutData.MyShortcutDataInfo> objects)
        {
            List<MyShortcutData.MyShortcutDataInfo> sortedList = new List<MyShortcutData.MyShortcutDataInfo>();

            // Папка
            foreach(var obj in objects.Where(obj => obj.obj is DefaultAsset))
            {
                sortedList.Add(obj);
            }

            // Сцена
            foreach(var obj in objects.Where(obj => obj.obj is SceneAsset))
            {
                sortedList.Add(obj);
            }

            // Скрипт
            foreach(var obj in objects.Where(obj => obj.obj is MonoScript))
            {
                sortedList.Add(obj);
            }

            // Префаб
            foreach(var obj in objects.Where(obj => PrefabUtility.IsPartOfAnyPrefab(obj.obj)))
            {
                sortedList.Add(obj);
            }

            // Картинка
            foreach(var obj in objects.Where(obj => obj.obj is Texture2D))
            {
                sortedList.Add(obj);
            }

            // Что-то другое
            foreach(var obj in objects.Where(obj => !sortedList.Contains(obj)))
            {
                sortedList.Add(obj);
            }

            return sortedList;
        }

        private void OnGUI()
        {
            List<MyShortcutData.MyShortcutDataInfo> dataInfoList = shortcutData.dataInfoList;

            #region Add Shortcut

            Rect addObjFieldRect = new Rect(1f, 0f, (position.width - 2f), 20f);
            DrawBackgroundBox(addObjFieldRect, AddFieldColor);

            EditorGUILayout.BeginHorizontal();
            {
                EditorGUIUtility.labelWidth = 115f;
                newObject = EditorGUILayout.ObjectField("Добавить ссылку:", newObject, typeof(Object), true, GUILayout.Width(230f));

                if (newObject != null && !dataInfoList.Exists(x => x.obj == newObject))
                {
					var firstEmptyIdx = dataInfoList.FindIndex(x => x.obj == null);
					var newDataInfo = new MyShortcutData.MyShortcutDataInfo(newObject);

					if (firstEmptyIdx < 0)
					{
						dataInfoList.Add(newDataInfo);
					}
					else
					{
						dataInfoList[firstEmptyIdx] = newDataInfo;
					}

                    shortcutData.dataInfoList = SortObjectsByType(dataInfoList);

					EditorUtility.SetDirty(shortcutData);
				}

                newObject = null;
            }
            EditorGUILayout.EndHorizontal();

            #endregion Add Shortcut

            #region Show list

            Rect listFieldRect = new Rect(1f, 21f, (position.width - 2f), position.height);
            DrawBackgroundBox(listFieldRect, ListFieldColor);

            EditorGUILayout.BeginHorizontal();
            {
				EditorGUILayout.LabelField("[Объект]");
            }
            EditorGUILayout.EndHorizontal();

            scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.Width(position.width), GUILayout.Height(position.height - 40f));
            {
                int showIdx = 0;
                MyShortcutData.MyShortcutDataInfo curInfo;
                StringBuilder labelSb = new StringBuilder();
                int deleteIdx = -1;

                for (var i = 0; i < dataInfoList.Count; ++i)
                {
                    curInfo = dataInfoList[i];

                    if (curInfo.obj == null)
                    {
                        continue;
                    }

                    EditorGUILayout.BeginHorizontal();
                    {
                        EditorGUI.BeginChangeCheck();
                        {
                            curInfo.obj = EditorGUILayout.ObjectField(curInfo.obj, typeof(Object), true);
                        }
                        if (EditorGUI.EndChangeCheck())
                        {
                            EditorUtility.SetDirty(shortcutData);
                        }
                        else if (GUILayout.Button("X", GUILayout.Width(20f)))
                        {
                            deleteIdx = i;
                        }

                        ++showIdx;
                    }
                    EditorGUILayout.EndHorizontal();
                }

                if (deleteIdx >= 0)
                {
                    dataInfoList.RemoveAt(deleteIdx);
                }
            }
            EditorGUILayout.EndScrollView();

            #endregion Show list
        }

        private void DrawBackgroundBox(Rect _rect, Color _color)
        {
            EditorGUI.HelpBox(_rect, null, MessageType.None);
            EditorGUI.DrawRect(_rect, _color);
        }
    }
}