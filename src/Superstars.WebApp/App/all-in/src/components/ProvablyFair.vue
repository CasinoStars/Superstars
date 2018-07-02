<template>
    <div class="wallet">
        <!-- errors of transactions !-->
        <div class="alert alert-danger" style="text-align: center;" v-if="errors.length > 0">
            <li v-for="e of errors" :key="e">{{e}}</li>
        </div>

        <!-- Upgrade -->

        <div class="form" style="letter-spacing: 2px; font-family: 'Courier New', sans-serif;">    
            <ul class="tab-group">
                <li style="color: white" class="tab active" > <h1 style="text-align: center;"> ProvavlyFair</h1> </li>
            </ul>
      
            <div class="tab-content">
                <h5 style="color: white"><span style="font-weight: bold; font-style: italic;">Client seeds:</span><br>{{seeds.clientSeed}}<br><br></h5>
                <h5 style="color: white"><span style="font-weight: bold; font-style: italic;">Crypted Server Seed:</span><br>{{seeds.cryptedServerSeed}}<br><br></h5>
                <h5 style="color: white"><span style="font-weight: bold; font-style: italic;">Nombre de dés générer par le hash:</span><br>{{seeds.nonce}}<br><br></h5>
                
                
                <h5 style="color: white"><span style="font-weight: bold; font-style: italic;">previous server seeds:</span><br>{{seeds.uncryptedPreviousServerSeed}}<br><br></h5>
                <h5 style="color: white"><span style="font-weight: bold; font-style: italic;">Previous Client Seed:</span><br>{{seeds.previousClientSeed}}<br><br></h5>
                <h5 style="color: white"><span style="font-weight: bold; font-style: italic;">Previous Crypted Server Seed:</span><br>{{seeds.previousCryptedServerSeed}}<br><br></h5>

                <form  @submit="UpdateSeeds($event)">
                        <div class="field-wrap">
                            <label style="margin-left: 45%; ">
                                <span style="font-weight: bold; font-style: italic;">
                                Seed Client<span class="req">*</span> 
                                </span>
                            </label><br><br>
                            <input  type="string" min="0" max="1000000" placeholder="Nouvelle seed client" v-model="clientSeeds" required autocomplete="off"/>
                        </div>

                    <button type="submit" class="button button-block">Changer</button>
                </form>

        </div><!-- tab-content -->
        </div> <!-- /form -->
        <!-- <div id="snackbar">Vous venez d'ajouter {{item.fakeCoins}} All`in Coins à votre portefeuille </div> -->
    </div>
</template>

<style lang="css">
  
</style>

<script>
import { mapActions } from 'vuex';
import ProvablyFairApiService from '../services/ProvablyFairApiService';


export default {
    data(){
        return {
            clientSeeds: '',
            seeds:  '',
            errors: [],
            Responses : [],
        };
    },

    mounted(){
        this.CreateSeeds();
        this.GetSeeds();
    },

    methods: {
        ...mapActions(['executeAsyncRequest']),

        async CreateSeeds(){
            await this.executeAsyncRequest(() => ProvablyFairApiService.CreateSeeds())
        },

        async GetSeeds(){
            this.seeds = await this.executeAsyncRequest(() => ProvablyFairApiService.GetSeeds());
        },


        async UpdateSeeds(e) {
            e.preventDefault();
            console.log("Test");
            var errors = [];
 
                    await this.executeAsyncRequest(() => ProvablyFairApiService.UpdateSeeds(this.clientSeeds));
                    this.GetSeeds();
        },

    }
}
</script>

<style lang="scss">
// @import "compass/css3";


$body-bg: #c1bdba;
$form-bg: #13232f;
$white: #ffffff;

$main: #777c7b;
$main-light: lighten($main,5%);
$main-dark: darken($main,5%);

$gray-light: #a0b3b0;
$gray: #ddd;

$thin: 300;
$normal: 400;
$bold: 600;
$br: 4px;

// *, *:before, *:after {
//   box-sizing: border-box;
// }
#snackbar {
    visibility: hidden;
    min-width: 250px;
    margin-left: -11%;
    background-color: #333;
    color: #fff;
    text-align: center;
    border-radius: 2px;
    padding: 16px;
    position: fixed;
    z-index: 1;
    left: 50%;
    bottom: 30px;
    font-size: 17px;
}

#snackbar.show {
    visibility: visible;
    -webkit-animation: fadein 0.5s, fadeout 0.5s 3s;
    animation: fadein 0.5s, fadeout 0.5s 3s;
}

@-webkit-keyframes fadein {
    from {bottom: 0; opacity: 0;} 
    to {bottom: 30px; opacity: 1;}
}

@keyframes fadein {
    from {bottom: 0; opacity: 0;}
    to {bottom: 30px; opacity: 1;}
}

@-webkit-keyframes fadeout {
    from {bottom: 30px; opacity: 1;} 
    to {bottom: 0; opacity: 0;}
}

@keyframes fadeout {
    from {bottom: 30px; opacity: 1;}
    to {bottom: 0; opacity: 0;}
}

.wallet html {
	overflow-y: scroll; 
}

.wallet body {
  background:$body-bg;
  font-family: 'Titillium Web', sans-serif;
}

.wallet .mytitle {
  text-decoration:none;
  color:$main;
  transition:.5s ease;
  &:hover {
    color:$main-dark;
  }
}

.form {
  background:rgba($form-bg,.9);
  padding: 40px;
  max-width:6000px;
  margin:40px auto;
  border-radius:$br;
  box-shadow:0 4px 10px 4px rgba($form-bg,.3);
}

.tab-group {
  list-style:none;
  padding:0;
  margin:0 0 40px 0;
  &:after {
    content: "";
    display: table;
    clear: both;
  }
  li a {
    display:block;
    text-decoration:none;
    padding:15px;
    background:rgba($gray-light,.25);
    color:$gray-light;
    font-size:20px;
    float:left;
    width:50%;
    text-align:center;
    cursor:pointer;
    transition:.5s ease;
    &:hover {
      background:$main-dark;
      color:$white;
    }
  }
  .active a {
    background:$main;
    color:$white;
  }
}

.tab-content > div:last-child {
  display:none;
}


.wallet h5 {
  text-align:center;
  color:$white;
  font-weight:$thin;
}

.wallet label {
  position:absolute;
  transform:translateY(6px);
  left:13px;
  color:$white;
  transition:all 0.25s ease;
  -webkit-backface-visibility: hidden;
  pointer-events: none;
  font-size:22px;
  .req {
  	margin:2px;
  	color:#1ab188;;
  }
}

.wallet input, textarea {
  font-size:22px;
  display:block;
  width:100%;
  height:100%;
  padding:5px 10px;
  background:none;
  background-image:none;
  border:1px solid $gray-light;
  color:$white;
  border-radius:0;
  transition:border-color .25s ease, box-shadow .25s ease;
  &:focus {
		outline:0;
		border-color:$main;
  }
}

.wallet textarea {
  border:2px solid $gray-light;
  resize: vertical;
}

.field-wrap {
  position:relative;
  margin-bottom:10px;
}


.button {
  border:0;
  outline:none;
  border-radius:0;
  padding:15px 0;
  font-size:2rem;
  font-weight:$bold;
  text-transform:uppercase;
  letter-spacing:.1em;
  background:$main;
  color:$white;
  transition:all.5s ease;
  -webkit-appearance: none;
  &:hover, &:focus {
    background:$main-dark;
  }
}

.button-block {
  display:block;
  width:100%;
}

.forgot {
  margin-top:-20px;
  text-align:right;
}
</style>
 
