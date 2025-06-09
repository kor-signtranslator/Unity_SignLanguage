// Assets/Plugins/WebGL/SpeechRecognition.jslib
mergeInto(LibraryManager.library, {
  StartSpeechRecognition: function() {
    var SR = window.SpeechRecognition || window.webkitSpeechRecognition;
    if (!SR) {
      console.warn("SpeechRecognition API를 지원하지 않는 브라우저입니다.");
      return;
    }
    var rec = new SR();
    rec.lang = 'ko-KR';
    rec.continuous = true;
    rec.interimResults = false;

    rec.onresult = function(event) {
      var text = event.results[event.resultIndex][0].transcript.trim();
      console.log("JS Recognized:", text);
      SendMessage('VoiceController', 'OnRecognized', text);
    };
    rec.onerror = function(err) {
      console.error("SpeechRecognition Error:", err);
    };
    rec.start();
  }
}); 

