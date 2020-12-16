using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.IO;
using UnityEngine.Events;

namespace Zetcil
{

    public class VarConfig : MonoBehaviour
    {
        public enum COperationType { None, Initialize, Runtime }

        public enum CLanguageType { Indonesian, English, Arabic, Korean, Japanese, Chinese }

        [Space(10)]
        public bool isEnabled;

        [Header("Operation Settings")]
        public COperationType OperationType;

        [Header("Directory Settings")]
        public string ConfigDirectory = "Config";
        public string CornerDirectory = "Corner";
        public string LanguageDirectory = "Languages";
        public string NotificationDirectory = "Notification";
        public string VisualNovelDirectory = "VisualNovel";
        public string SessionDirectory = "Session";

        TextAsset cornerAsset;
        TextAsset languageAsset;
        TextAsset notificationAsset;
        TextAsset visualNovelAsset;

        // Start is called before the first frame update
        void Start()
        {
            if (isEnabled)
            {
                if (OperationType == COperationType.Initialize)
                {
                    InitializeCorner();
                    InitializeLanguage();
                    InitializeNotification();
                    InitializeVisualNovel();
                }
                if (OperationType == COperationType.Runtime)
                {
                }
            }
        }

        // Update is called once per frame
        void Update()
        {

        }

        public string GetDirectory(string aDirectoryName)
        {
            if (!Directory.Exists(Application.streamingAssetsPath + "/" + aDirectoryName + "/"))
            {
                Directory.CreateDirectory(Application.streamingAssetsPath + "/" + aDirectoryName + "/");
            }
            return Application.streamingAssetsPath + "/" + aDirectoryName + "/";
        }

        public string GetSessionDirectory()
        {
            return GetDirectory(SessionDirectory);
        }

        public string GetConfigDirectory()
        {
            return GetDirectory(ConfigDirectory);
        }

        public string GetCornerDirectory()
        {
            return GetDirectory(CornerDirectory);
        }

        public string GetLanguageDirectory()
        {
            return GetDirectory(LanguageDirectory);
        }

        public string GetNotificationDirectory()
        {
            return GetDirectory(NotificationDirectory);
        }

        public string GetVisualNovelDirectory()
        {
            return GetDirectory(VisualNovelDirectory);
        }

        public string SetXMLOpenTag(string aTag, int aTab = 1)
        {
            string opentag;
            string tabparent = "\t";
            string tabchild = "\t\t";
            if (aTab == 2)
            {
                tabparent = "\t\t";
                tabchild = "\t\t\t\t";
            }
            else if (aTab == 3)
            {
                tabparent = "\t\t\t";
                tabchild = "\t\t\t\t\t\t";
            }

            opentag = tabparent + "<" + aTag + ">\n";
            return opentag;
        }

        public string SetXMLCloseTag(string aTag, int aTab = 1)
        {
            string closetag;
            string tabparent = "\t";
            string tabchild = "\t\t";
            if (aTab == 2)
            {
                tabparent = "\t\t";
                tabchild = "\t\t\t\t";
            }
            else if (aTab == 3)
            {
                tabparent = "\t\t\t";
                tabchild = "\t\t\t\t\t\t";
            }

            closetag = tabparent + "</" + aTag + ">\n";
            return closetag; 
        }

        public string SetXMLValueSingle(string aTag, string aValue, int aTab = 1)
        {
            string opentag, contenttag, closetag;
            string tabparent = "\t";
            if (aTab == 2)
            {
                tabparent = "\t\t";
            }
            else if (aTab == 3)
            {
                tabparent = "\t\t\t";
            }
            opentag = tabparent + "<" + aTag + ">";
            contenttag = aValue;
            closetag = "</" + aTag + ">\n";

            return opentag + contenttag + closetag;
        }

        public string SetXMLValue(string aTag, string aValue, int aTab = 1, bool aUseNewRow = true)
        {
            string opentag, contenttag, closetag;
            string tabparent = "\t";
            string tabchild = "\t\t";
            string newrow = "\n";
            if (aTab == 2)
            {
                tabparent = "\t\t";
                tabchild = "\t\t\t\t";
            }
            else if (aTab == 3)
            {
                tabparent = "\t\t\t";
                tabchild = "\t\t\t\t\t\t";
            }

            if (!aUseNewRow)
            {
                newrow = "";
            }

            opentag = tabparent + "<" + aTag + ">" + newrow;
            contenttag = tabchild + aValue + newrow;
            closetag = tabparent + "</" + aTag + ">" + newrow;

            return opentag + contenttag + closetag;
        }

        public void InitializeLanguage()
        {
            languageAsset = (TextAsset)Resources.Load(LanguageDirectory + "/English", typeof(TextAsset));
            SaveLanguageFile("English", languageAsset.ToString());

            languageAsset = (TextAsset)Resources.Load(LanguageDirectory + "/Indonesian", typeof(TextAsset));
            SaveLanguageFile("Indonesian", languageAsset.ToString());

            languageAsset = (TextAsset)Resources.Load(LanguageDirectory + "/Arabic", typeof(TextAsset));
            SaveLanguageFile("Arabic", languageAsset.ToString());

            languageAsset = (TextAsset)Resources.Load(LanguageDirectory + "/Korean", typeof(TextAsset));
            SaveLanguageFile("Korean", languageAsset.ToString());

            languageAsset = (TextAsset)Resources.Load(LanguageDirectory + "/Japanese", typeof(TextAsset));
            SaveLanguageFile("Japanese", languageAsset.ToString());

            languageAsset = (TextAsset)Resources.Load(LanguageDirectory + "/Chinese", typeof(TextAsset));
            SaveLanguageFile("Chinese", languageAsset.ToString());
        }

        public void SaveLanguageFile(string aLanguageID, string aLanguageData)
        {
            string DirName = GetDirectory(LanguageDirectory);
            var sr = File.CreateText(DirName + aLanguageID + ".xml");
            sr.WriteLine(aLanguageData);
            sr.Close();
        }

        public void InitializeNotification()
        {
            notificationAsset = (TextAsset)Resources.Load(NotificationDirectory + "/English", typeof(TextAsset));
            SaveNotificationFile("English", notificationAsset.ToString());

            notificationAsset = (TextAsset)Resources.Load(NotificationDirectory + "/Indonesian", typeof(TextAsset));
            SaveNotificationFile("Indonesian", notificationAsset.ToString());

            notificationAsset = (TextAsset)Resources.Load(NotificationDirectory + "/Arabic", typeof(TextAsset));
            SaveNotificationFile("Arabic", notificationAsset.ToString());

            notificationAsset = (TextAsset)Resources.Load(NotificationDirectory + "/Korean", typeof(TextAsset));
            SaveNotificationFile("Korean", notificationAsset.ToString());

            notificationAsset = (TextAsset)Resources.Load(NotificationDirectory + "/Japanese", typeof(TextAsset));
            SaveNotificationFile("Japanese", notificationAsset.ToString());

            notificationAsset = (TextAsset)Resources.Load(NotificationDirectory + "/Chinese", typeof(TextAsset));
            SaveNotificationFile("Chinese", notificationAsset.ToString());
        }

        public void SaveNotificationFile(string aLanguageID, string aLanguageData)
        {
            string DirName = GetDirectory(NotificationDirectory);
            var sr = File.CreateText(DirName + aLanguageID + ".xml");
            sr.WriteLine(aLanguageData);
            sr.Close();
        }

        public void InitializeCorner()
        {
            cornerAsset = (TextAsset)Resources.Load(CornerDirectory + "/English", typeof(TextAsset));
            SaveCornerFile("English", cornerAsset.ToString());

            cornerAsset = (TextAsset)Resources.Load(CornerDirectory + "/Indonesian", typeof(TextAsset));
            SaveCornerFile("Indonesian", cornerAsset.ToString());

            cornerAsset = (TextAsset)Resources.Load(CornerDirectory + "/Arabic", typeof(TextAsset));
            SaveCornerFile("Arabic", cornerAsset.ToString());

            cornerAsset = (TextAsset)Resources.Load(CornerDirectory + "/Korean", typeof(TextAsset));
            SaveCornerFile("Korean", cornerAsset.ToString());

            cornerAsset = (TextAsset)Resources.Load(CornerDirectory + "/Japanese", typeof(TextAsset));
            SaveCornerFile("Japanese", cornerAsset.ToString());

            cornerAsset = (TextAsset)Resources.Load(CornerDirectory + "/Chinese", typeof(TextAsset));
            SaveCornerFile("Chinese", cornerAsset.ToString());
        }

        public void SaveCornerFile(string aLanguageID, string aLanguageData)
        {
            string DirName = GetDirectory(CornerDirectory);
            var sr = File.CreateText(DirName + aLanguageID + ".xml");
            sr.WriteLine(aLanguageData);
            sr.Close();
        }

        public void InitializeVisualNovel()
        {
            visualNovelAsset = (TextAsset)Resources.Load(VisualNovelDirectory + "/English", typeof(TextAsset));
            SaveVisualNovelFile("English", visualNovelAsset.ToString());

            visualNovelAsset = (TextAsset)Resources.Load(VisualNovelDirectory + "/Indonesian", typeof(TextAsset));
            SaveVisualNovelFile("Indonesian", visualNovelAsset.ToString());

            visualNovelAsset = (TextAsset)Resources.Load(VisualNovelDirectory + "/Arabic", typeof(TextAsset));
            SaveVisualNovelFile("Arabic", visualNovelAsset.ToString());

            visualNovelAsset = (TextAsset)Resources.Load(VisualNovelDirectory + "/Korean", typeof(TextAsset));
            SaveVisualNovelFile("Korean", visualNovelAsset.ToString());

            visualNovelAsset = (TextAsset)Resources.Load(VisualNovelDirectory + "/Japanese", typeof(TextAsset));
            SaveVisualNovelFile("Japanese", visualNovelAsset.ToString());

            visualNovelAsset = (TextAsset)Resources.Load(VisualNovelDirectory + "/Chinese", typeof(TextAsset));
            SaveVisualNovelFile("Chinese", visualNovelAsset.ToString());
        }

        public void SaveVisualNovelFile(string aLanguageID, string aLanguageData)
        {
            string DirName = GetDirectory(VisualNovelDirectory);
            var sr = File.CreateText(DirName + aLanguageID + ".xml");
            sr.WriteLine(aLanguageData);
            sr.Close();
        }

    }
}
