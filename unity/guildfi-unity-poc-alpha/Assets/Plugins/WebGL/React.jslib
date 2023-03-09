mergeInto(LibraryManager.library, {
  SetModelLoadPercentage: function (modelLoadPercentage) {
    window.dispatchReactUnityEvent("SetModelLoadPercentage", modelLoadPercentage);
  },
});
