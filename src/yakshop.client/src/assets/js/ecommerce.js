export function initSwiper() {
  var swiper = new Swiper(".main-swiper", {
    speed: 500,
    pagination: {
      el: ".swiper-pagination",
      clickable: true,
    },
  });
};

(function ($) {
  "use strict";
  setTimeout(function () {}, 1500);
  var initPreloader = function () {
    $(document).ready(function ($) {
      var body = $("body");
      body.addClass("preloader-site");
    });

    $(window).load(function () {
      $(".preloader-wrapper").fadeOut();
      $("body").removeClass("preloader-site");
    });
  };

  $(document).ready(function () {
    initPreloader();
  });
})(jQuery);