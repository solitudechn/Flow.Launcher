using Flow.Launcher.Plugin.SharedModels;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Flow.Launcher.Plugin
{
    /// <summary>
    /// Public APIs that plugin can use
    /// </summary>
    public interface IPublicAPI
    {
        /// <summary>
        /// Change Flow.Launcher query
        /// </summary>
        /// <param name="query">query text</param>
        /// <param name="requery">
        /// force requery By default, Flow Launcher will not fire query if your query is same with existing one. 
        /// Set this to true to force Flow Launcher requerying
        /// </param>
        void ChangeQuery(string query, bool requery = false);

        /// <summary>
        /// Restart Flow Launcher
        /// </summary>
        void RestartApp();

        /// <summary>
        /// Save all Flow Launcher settings
        /// </summary>
        void SaveAppAllSettings();

        /// <summary>
        /// Reloads any Plugins that have the 
        /// IReloadable implemented. It refeshes
        /// Plugin's in memory data with new content
        /// added by user.
        /// </summary>
        Task ReloadAllPluginData();

        /// <summary>
        /// Check for new Flow Launcher update
        /// </summary>
        void CheckForNewUpdate();

        /// <summary>
        /// Show the error message using Flow's standard error icon.
        /// </summary>
        /// <param name="title">Message title</param>
        /// <param name="subTitle">Optional message subtitle</param>
        void ShowMsgError(string title, string subTitle = "");

        /// <summary>
        /// Show message box
        /// </summary>
        /// <param name="title">Message title</param>
        /// <param name="subTitle">Message subtitle</param>
        /// <param name="iconPath">Message icon path (relative path to your plugin folder)</param>
        void ShowMsg(string title, string subTitle = "", string iconPath = "");

        /// <summary>
        /// Show message box
        /// </summary>
        /// <param name="title">Message title</param>
        /// <param name="subTitle">Message subtitle</param>
        /// <param name="iconPath">Message icon path (relative path to your plugin folder)</param>
        /// <param name="useMainWindowAsOwner">when true will use main windows as the owner</param>
        void ShowMsg(string title, string subTitle, string iconPath, bool useMainWindowAsOwner = true);

        /// <summary>
        /// Open setting dialog
        /// </summary>
        void OpenSettingDialog();

        /// <summary>
        /// Get translation of current language
        /// You need to implement IPluginI18n if you want to support multiple languages for your plugin
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        string GetTranslation(string key);

        /// <summary>
        /// Get all loaded plugins 
        /// </summary>
        /// <returns></returns>
        List<PluginPair> GetAllPlugins();

        /// <summary>
        /// Fired after global keyboard events
        /// if you want to hook something like Ctrl+R, you should use this event
        /// </summary>
        event FlowLauncherGlobalKeyboardEventHandler GlobalKeyboardEvent;

        /// <summary>
        /// Fuzzy Search the string with the given query. This is the core search mechanism Flow uses
        /// </summary>
        /// <param name="query">Query string</param>
        /// <param name="stringToCompare">The string that will be compared against the query</param>
        /// <returns>Match results</returns>
        MatchResult FuzzySearch(string query, string stringToCompare);

        /// <summary>
        /// Http download the spefic url and return as string
        /// </summary>
        /// <param name="url">URL to call Http Get</param>
        /// <param name="token">Cancellation Token</param>
        /// <returns>Task to get string result</returns>
        Task<string> HttpGetStringAsync(string url, CancellationToken token = default);

        /// <summary>
        /// Http download the spefic url and return as stream
        /// </summary>
        /// <param name="url">URL to call Http Get</param>
        /// <param name="token">Cancellation Token</param>
        /// <returns>Task to get stream result</returns>
        Task<Stream> HttpGetStreamAsync(string url, CancellationToken token = default);

        /// <summary>
        /// Download the specific url to a cretain file path
        /// </summary>
        /// <param name="url">URL to download file</param>
        /// <param name="token">place to store file</param>
        /// <returns>Task showing the progress</returns>
        Task HttpDownloadAsync([NotNull] string url, [NotNull] string filePath, CancellationToken token = default);

        /// <summary>
        /// Add ActionKeyword for specific plugin
        /// </summary>
        /// <param name="pluginId">ID for plugin that needs to add action keyword</param>
        /// <param name="newActionKeyword">The actionkeyword that is supposed to be added</param>
        void AddActionKeyword(string pluginId, string newActionKeyword);

        /// <summary>
        /// Remove ActionKeyword for specific plugin
        /// </summary>
        /// <param name="pluginId">ID for plugin that needs to remove action keyword</param>
        /// <param name="newActionKeyword">The actionkeyword that is supposed to be removed</param>
        void RemoveActionKeyword(string pluginId, string oldActionKeyword);

        /// <summary>
        /// Log debug message
        /// Message will only be logged in Debug mode
        /// </summary>
        void LogDebug(string className, string message, [CallerMemberName] string methodName = "");

        /// <summary>
        /// Log info message
        /// </summary>
        void LogInfo(string className, string message, [CallerMemberName] string methodName = "");

        /// <summary>
        /// Log warning message
        /// </summary>
        void LogWarn(string className, string message, [CallerMemberName] string methodName = "");

        /// <summary>
        /// Log an Exception. Will throw if in debug mode so developer will be aware, 
        /// otherwise logs the eror message. This is the primary logging method used for Flow 
        /// </summary>
        void LogException(string className, string message, Exception e, [CallerMemberName] string methodName = "");

        /// <summary>
        /// Load JsonStorage for current plugin. This is the method used to load settings from json in Flow
        /// </summary>
        /// <typeparam name="T">Type for deserialization</typeparam>
        /// <returns></returns>
        T LoadJsonStorage<T>() where T : new();

        /// <summary>
        /// Save JsonStorage for current plugin. This is the method used to save settings to json in Flow.Launcher
        /// This method will save the original instance loaded with LoadJsonStorage.
        /// </summary>
        /// <typeparam name="T">Type for Serialization</typeparam>
        /// <returns></returns>
        void SaveJsonStorage<T>() where T : new();

        /// <summary>
        /// Save JsonStorage for current plugin. This is the method used to save settings to json in Flow.Launcher
        /// This method will override the original class instance loaded from LoadJsonStorage
        /// </summary>
        /// <typeparam name="T">Type for Serialization</typeparam>
        /// <returns></returns>
        void SaveJsonStorage<T>(T settings) where T : new();

        /// <summary>
        /// Backup the JsonStorage you loaded from LoadJsonStorage
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="settings"></param>
        void BackupJsonStorage<T>() where T : new();
    }
}
