mergeInto(LibraryManager.library, {

  CopyText: function (str) {
    const textToCopy = UTF8ToString(str);
    navigator.clipboard.writeText(textToCopy).then(function() {
  /* clipboard successfully set */
}, function() {
  /* clipboard write failed */
  window.alert("Failed to copy text! It's possible you are using an unsupported browser such as Safari. Please copy your code manually: " + textToCopy);
});

  },

    CopyTextApple: function (str) {
      const method = async () => {

      var type = "text/plain";
      var blob = new Blob([`test data`], { type });
      var data = [new ClipboardItem({ [type]: blob })];

      await navigator.clipboard.write(UTF8ToString(str)).then(
          function () {
          /* success */
          },
          function () {
          /* failure */
            window.alert("Failed to copy text!");
          }
      );

      }
}

      /*
      const clipboardItem = new ClipboardItem({
      'text/plain': someAsyncMethod().then((result) => {

      /**
       * We have to return an empty string to the clipboard if something bad happens, otherwise the
       * return type for the ClipBoardItem is incorrect.
       */
       /*
      if (!result) 
      {
          return new Promise(async (resolve) => {
            resolve(new Blob[``]())
          })
      }

    const copyText = `test data`
        return new Promise(async (resolve) => {
            resolve(new Blob([copyText]))
          })
        }),
    })
    // Now, we can write to the clipboard in Safari
      await navigator.clipboard.write([clipboardItem]).then(function() {
        //Success!
      }, function() {
        //Failed!
        window.alert("Failed to copy text!");
      });
  },
  */

});