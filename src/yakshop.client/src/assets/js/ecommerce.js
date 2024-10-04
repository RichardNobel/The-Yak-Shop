(function($) {

    "use strict";
    setTimeout(function(){ }, 1500);
    var initPreloader = function() {
      $(document).ready(function($) {
      var Body = $('body');
          Body.addClass('preloader-site');
      });
      
      $(window).load(function() {
          $('.preloader-wrapper').fadeOut();
          $('body').removeClass('preloader-site');
      });
    }    
  
    var initSwiper = function() {  
      var swiper = new Swiper(".main-swiper", {
        speed: 500,
        pagination: {
          el: ".swiper-pagination",
          clickable: true,
        },
      });
    }
  
    // input spinner
    var initProductQty = function(){
  
      $('.product-qty').each(function(){
  
        var $el_product = $(this);
        var quantity = 0;
  
        $el_product.find('.quantity-right-plus').click(function(e){
            e.preventDefault();
            var quantity = parseInt($el_product.find('#quantity').val());
            $el_product.find('#quantity').val(quantity + 1);
        });
  
        $el_product.find('.quantity-left-minus').click(function(e){
            e.preventDefault();
            var quantity = parseInt($el_product.find('#quantity').val());
            if(quantity>0){
              $el_product.find('#quantity').val(quantity - 1);
            }
        });
  
      });
  
    }
  
  
    // document ready
    $(document).ready(function() {
    
        initPreloader();
        initSwiper();
        initProductQty();
        
     
    }); // End of a document
  
  })(jQuery);