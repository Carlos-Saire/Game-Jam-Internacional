mergeInto(LibraryManager.library, {
  SetTime: function (text) {
    // Convert the C# memory pointer to a JS String
    window.dispatchReactUnityEvent("SetTime", UTF8ToString(text));
  },
  SetCandys: function (text) {
    window.dispatchReactUnityEvent("SetCandys", UTF8ToString(text));
  },
  SetLife: function (text) {
    window.dispatchReactUnityEvent("SetLife", UTF8ToString(text));
  }
});