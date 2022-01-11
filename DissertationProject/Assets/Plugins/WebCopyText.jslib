mergeInto(LibraryManager.library, {

  CopyText: function (str) {
    navigator.clipboard.writeText(Pointer_stringify(str)).then(function() {
  /* clipboard successfully set */
  window.alert("Successful");
}, function() {
  /* clipboard write failed */
  window.alert("Failed!");
});

  },

});