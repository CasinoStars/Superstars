
<template>
<div class="account">
    <div class="menu">      
        <div class="carre">
            <h5 style = "padding-top: 60%; text-align: center; color: white;"> MyUserName</h5> 
            <img src="../img/LOGO1.png" alt="yams" class="imglogo">  
         </div>
             
         <div class="submenu">
           <bouton>  Profil </bouton> </div>
         <div class="submenu"><bouton>Porte monnaie</bouton></div>
         <div class="submenu"><bouton>ProvablyFair</bouton></div>
         <div class="submenu"><bouton>Support</bouton></div>
         <div class="submenu"><bouton>Sécuriter</bouton></div>
        
    </div>
    <wallet/>
</div>
</template>



<script>

import Wallet from './Wallet.vue'



  export default {
    components: {
         Wallet
    },

    methods: {
      handleOpen(key, keyPath) {
        console.log(key, keyPath);
      },
      handleClose(key, keyPath) {
        console.log(key, keyPath);
      }
    }
  } 
</script>


<style lang="scss">

.account .wallet {
    margin-left: 10%;
    margin-top: -43.5%;
}

.account .submenu {

     padding-top: 3%;
     text-align: center;
     color: white;
	 width:347px;
	 height:50px;
     border-style: solid;
     border-width: 1px;   
     border-color: black;
	 background:#545c64;

}
///.carre > .submenu {
    
///}

.account .menu {
       margin-top:3%;
    margin-left:12%;
	width:350px;
	height:800px;
    border-style: solid;
    border-width: 2px;   
    border-color: black;
	background:#545c64;
    border-radius: 10px;
}

.account .imglogo {
    margin-top: -90%;
    margin-left: 22%;
    width: 200px;
    height: 200px;
}


</style>