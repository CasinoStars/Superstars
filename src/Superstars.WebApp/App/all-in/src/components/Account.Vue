
<template>
<div class="account">
    <div class="row">
        <div class="menu col-md-3">      
            <div class="carre">
                <img src="../img/LOGO1.png" alt="yams" class="imglogo">  
                <h1 style="text-align: center; color: white;"> admin</h1> 
            </div>
                
            <!-- <div class="submenu" ><div class="myButton"><button>  Profil </button></div> </div> -->
            <div class="submenu"><button class="button button-block" v-on:click=" TransferOn()">Transferts</button></div>
            <div class="submenu"><button class="button button-block" v-on:click=" WalletOn()">Portefeuille</button></div>
            <div class="submenu"><button class="button button-block" v-on:click="ProvablyFairOn()">Provably Fair</button></div>
            <div class="submenu"><button class="button button-block" v-on:click="SupportPageOn()">Support</button></div>
            <div class="submenu"><button class="button button-block" v-on:click="SecuriterOn()">Sécurité</button></div>
            
        </div>
        <div class="col-md-5" style="margin-left: 3.5%;">
            <div class = "improtedView"> 
                <transfer v-if="this.isTransferOn == true"/>
                <wallet v-if="this.isWallet == true"/>
                <div class ="support"><support v-if="this.isSupportPageOn == true"/></div>
                <div class="settings"><Settings v-if="this.isSecuriterOn == true"/></div>
                <div class="provablyFair"> <ProvablyFair v-if="this.isProvablyFair == true"/> </div>
           </div>
        </div>
    </div>
</div>
</template>

<script>

import Wallet from './Wallet.vue'
import Support from './Support.vue'
import ProvablyFair from './ProvablyFair.vue'
import Transfer from './Transfer.vue'
import Settings from './Settings.vue'



  export default {
    data() {
        return {
        isWallet: "",
        isProvablyFair: "",
        isSupportPageOn: "",
        isSecuriterOn: "",
        isProfilOn:"",
        isTransferOn:"",
        };
    },

  components: {
         Wallet,
         Transfer,
         Support,         
         ProvablyFair,
         Settings
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
         this.isProvablyFair = false;
         this.isSupportPageOn = false;
         this.isSecuriterOn = false;
         this.isProfilOn = false;
         this.isTransferOn = false;

      },
       ProvablyFairOn() {
         this.isWallet = false;
         this.isProvablyFair = true;
         this.isSupportPageOn = false;
         this.isSecuriterOn = false;
         this.isProfilOn = false;
         this.isTransferOn = false;
      },
       SupportPageOn() {
         this.isWallet = false;
         this.isProvablyFair = false;
         this.isSupportPageOn = true;
         this.isSecuriterOn = false;
         this.isProfilOn = false;
         this.isTransferOn = false;
      },
       SecuriterOn() {
         this.isWallet = false;
         this.isProvablyFair = false;
         this.isSupportPageOn = false;
         this.isSecuriterOn = true;
         this.isProfilOn = false;
         this.isTransferOn = false;
      },
       TransferOn() {
         this.isWallet = false;
         this.isProvablyFair = false;
         this.isSupportPageOn = false;
         this.isProfilOn = false;
         this.isTransferOn = true;
         this.isSecuriterOn = false;

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

.account .importedView {
width: 600px;
}

.account .submenu .myButton {
    border-radius: 10px;
    width: 100%;
    height: 100%;
}

 .submenu .button-block {
    display: block;
    width: 100%;
    height: 100%;
    font-size: 150%;
    background-color:rgba($form-bg, 0.9);
    color:white;
    border-radius: 10px;
    border-color:lightblue;
  }


   .submenu  .button-block:hover{
      opacity: 1;
      background-color: #2695F3;
  }


    .button-block {
    display: block;
    width: 100%;
    height: 100%;
    font-size: 150%;
    background-color:rgb(77, 179, 77);
    color:white;
    border-radius: 10px;
    border-color:black;
    opacity: 0.9;
  }

  


.account .submenu {
     box-shadow: 0 4px 10px 4px rgba($form-bg, 0.3);
     background-color: rgba($form-bg, 0.9);
     //padding-top: 3%;
     text-align: center;
     color: white;
	 width:100%;
	 height:50px;
     border-style: solid;
     border-width: 2px;   
     border-color: black;
     border-radius: 10px;


}


.account .provablyFair, .account .settings, .account .support {
    margin-top: 7.5%;
}



.account .menu {
    box-shadow: 0 4px 10px 4px rgba($form-bg, 0.3);
     background: rgba($form-bg, 0.9);
       margin-top:3%;
    margin-left:10%;
	width:350px;
	height:900px;
    border-style: solid;
    border-width: 2px;   
    border-color: black;
	//background: $main-dark;
    border-radius: 10px;
    opacity: 0.9;
    min-height: 800px;
}

.account .imglogo {
    //padding-left:50%;
    width: 105%;
    height: 100%;
}


</style>