
<template>
<div class="account">
    <div class="row">
        <div class="menu col-md-3">      
            <div class="carre">
                <img src="../img/LOGO1.png" alt="yams" class="imglogo">  
                <h5 style="text-align: center; color: white;"> MyUserName</h5> 
            </div>
                
            <div class="submenu" >
            <bouton >  Profil </bouton> </div>
            <div class="submenu"><button v-on:click=" WalletOn()">Porte monnaie</button></div>
            <div class="submenu"><button>ProvablyFair</button></div>
            <div class="submenu"><button>Support</button></div>
            <div class="submenu"><button>Sécuriter</button></div>
            
        </div>
        <div class="col-md-5">
            <wallet v-if="this.isWallet == true"/>
        </div>
    </div>
</div>
</template>



<script>

import Wallet from './Wallet.vue'



  export default {
    data() {
        return {
        isWallet: ""
        };
    },

  components: {
         Wallet
    },


  

    methods: {
      handleOpen(key, keyPath) {
        console.log(key, keyPath);
      },
      handleClose(key, keyPath) {
        console.log(key, keyPath);      
      },
          WalletOn() {
         this.isWallet = true;
      } 
    }
  } 
</script>


<style lang="scss">

 $body-bg: #c1bdba;
  $form-bg: #13232f;
  $white: #ffffff;

  $main: #777c7b;
  $main-light: lighten($main, 5%);
  $main-dark: darken($main, 5%);

  $gray-light: #a0b3b0;
  $gray: #ddd;

  $thin: 300;
  $normal: 400;
  $bold: 600;
  $br: 4px;

.account .submenu {
     box-shadow: 0 4px 10px 4px rgba($form-bg, 0.3);
     background: rgba($form-bg, 0.9);
     padding-top: 3%;
     opacity: 0.6;
     text-align: center;
     color: white;
	 width:100%;
	 height:50px;
     border-style: solid;
     border-width: 1px;   
     border-color: black;
	// background:#545c64;

}
///.carre > .submenu {
    
///}

.account .menu {
    box-shadow: 0 4px 10px 4px rgba($form-bg, 0.3);
     background: rgba($form-bg, 0.9);
       margin-top:3%;
    margin-left:10%;
	width:350px;
	height:800px;
    border-style: solid;
    border-width: 2px;   
    border-color: black;
	//background: $main-dark;
    border-radius: 10px;
    opacity: 0.9;
}

.account .imglogo {
    //padding-left:50%;
    width: 105%;
    height: 100%;
}


</style>