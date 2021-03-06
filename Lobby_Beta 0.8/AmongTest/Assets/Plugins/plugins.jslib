mergeInto(LibraryManager.library, {
 
   Alert: function (str) {
     window.alert(Pointer_stringify(str));
   },
 
   Console: function (str) {
     console.log(Pointer_stringify(str));
   },
 
   Warn: function (str) {
     console.warn(Pointer_stringify(str));
   },
 
   Error: function (str) {
     console.error(Pointer_stringify(str));
   }
 });