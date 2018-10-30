<template>
    <div class="provablyfair">
        <!-- errors of transactions !-->
        <div class="alert alert-danger" style="text-align: center;" v-if="errors.length > 0">
            <li v-for="e of errors" :key="e">{{e}}</li>
        </div>

        <!-- Upgrade -->

        <div class="form" style="letter-spacing: 2px; font-family: 'Courier New', sans-serif;">    

      <ul class="tab-group">
                <li style="color: white" class="tab active" v-if="this.view == 'provablyFair'"><a v-on:click="ChangeView('provablyFair')">Seeds</a></li>
                <li style="color: white" class="tab" v-else><a v-on:click="ChangeView('provablyFair')">Seeds</a></li>

                <li style="color: white" class="tab active" v-if="this.view == 'provablyFairTest'"><a v-on:click="ChangeView('provablyFairTest')">Retrouver les dés</a></li>
                <li style="color: white" class="tab" v-else><a v-on:click="ChangeView('provablyFairTest')">Retrouver les dés</a></li>
                
                
                <li style="color: white" class="tab active" v-if="this.view == 'provablyFairCode'"><a v-on:click="ChangeView('ProvablyFairCode')" style="margin-leftg: 25%;">Code</a></li>
                <li style="color: white" class="tab" v-else><a v-on:click="ChangeView('provablyFairCode')" style="margin-leftg: 25%;">Code</a></li>
            </ul>

            <div style="color: white" v-if="this.view == 'provablyFair'">
                <div class="tab-content">
                    <h5 style="color: white"><span style="font-weight: bold; font-style: italic;">Clef client:</span><br>{{seeds.clientSeed}}<br><br></h5>
                    <h5 style="color: white"><span style="font-weight: bold; font-style: italic;">Clef Serveur:</span><br>{{seeds.cryptedServerSeed}}<br><br></h5>
                    <h5 style="color: white"><span style="font-weight: bold; font-style: italic;">Nombre de dés générer par le hash:</span><br>{{seeds.nonce}}<br><br></h5>
                    
                    
                    <h5 style="color: white"><span style="font-weight: bold; font-style: italic;">Précedente clef serveur:</span><br>{{seeds.uncryptedPreviousServerSeed}}<br><br></h5>
                    <h5 style="color: white"><span style="font-weight: bold; font-style: italic;">Précedente clef client:</span><br>{{seeds.previousClientSeed}}<br><br></h5>
                    <h5 style="color: white"><span style="font-weight: bold; font-style: italic;">Précedente clef serveir crypté:</span><br>{{seeds.previousCryptedServerSeed}}<br><br></h5>

                    <form  @submit="UpdateSeeds($event)">
                            <div class="field-wrap">
                                <label style="margin-left: 45%; ">
                                    <span style="font-weight: bold; font-style: italic;">
                                    Clef client<span class="req">*</span> 
                                    </span>
                                </label><br><br>
                                <input  type="string" min="0" max="1000000" placeholder="Nouvelle seed client" v-model="clientSeeds" required autocomplete="off"/>
                            </div>

                        <button type="submit" class="button button-block">Changer</button>
                    </form>

                </div><!-- tab-content -->
            </div>








<div style="color: white" v-if="this.view == 'provablyFairTest'">
                <div class="tab-content">
                    <h5 style="color: white"><span style="font-weight: bold; font-style: italic;">Test:</span><br>{{seeds.clientSeed}}<br><br></h5>
        

                    <form  @submit="RetriveDicesFromSeeds($event)">
                            <div class="field-wrap">
                                <label style="margin-left: 45%; ">
                                    <span style="font-weight: bold; font-style: italic;">
                                    Seed Client<span class="req">*</span> 
                                    </span>
                                </label><br><br>
                                <input  type="string" min="0" max="1000000" placeholder="Nouvelle seed client" v-model="clientSeedTest" required autocomplete="off"/>
                            </div>
                             <div class="field-wrap">
                                <label style="margin-left: 45%; ">
                                    <span style="font-weight: bold; font-style: italic;">
                                    Seed Server<span class="req">*</span> 
                                    </span>
                                </label><br><br>
                                <input  type="string" min="0" max="1000000" placeholder="Nouvelle seed client" v-model="serverSeedTest" required autocomplete="off"/>
                            </div>
                             <div class="field-wrap">
                                <label style="margin-left: 45%; ">
                                    <span style="font-weight: bold; font-style: italic;">
                                    Nombre de dés<span class="req">*</span> 
                                    </span>
                                </label><br><br>
                                <input  type="int" min="0" max="1000000" placeholder="Nouvelle seed client" v-model="nbOfDices" required autocomplete="off"/>
                            </div>

                        <button type="submit" class="button button-block">Retrouver les dés</button>
                    </form>
                        <div v-for="(i,index) of (dicesFromSeeds)" :key="index" style="display: inline;"> {{i}}</div>
                </div><!-- tab-content -->
            </div>








          
 <div class="tab-content" v-else>
<h6 style="color: white"><span style="font-weight: bold; font-style: italic;"><pre style="color: white">
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace Rextester
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //DicesFromSeeds will retrieve all the dices generate by the provably fair
            // you have tu put your ServerSeed ClientSeed and the number of dices generated by your seeds in arguements
              List< int > dicesFromSeeds = HashManager.RetriveDicesFromSeeds("YourServerSeedHere", "YourClientSeedHere",0); 

            foreach 
            (var item in dicesFromSeeds)
            {
                Console.WriteLine(item);
            }
        }
}
    
    public class HashManager
    {
        public static string getHashSha512(string text)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(text);
            HashAlgorithm hashstring = SHA512.Create();
            byte[] hash = hashstring.ComputeHash(bytes);
            string hashString = string.Empty;
            foreach (byte x in hash)
            {
                hashString += String.Format("{0:x2}", x);
            }
            return hashString;
        }
       
        <public static List< int > RetriveDicesFromSeeds(string serveurSeed, string clientSeed, int nbOfDices)
        {
            List< int > retriveDicesFromSeeds = new List< int >();
            for (int i = 0; i < nbOfDices; i++)
            {
                retriveDicesFromSeeds.Add(HashManager.GetDiceFromHash(serveurSeed, clientSeed, i));
            }
            return retriveDicesFromSeeds;
        }
        public static int GetDiceFromHash(string serverSeed, string clientSeed, int nonce)
        {
            string clientSeedWithNonce = clientSeed + nonce.ToString();
            string hash = getHashSha512(serverSeed + clientSeedWithNonce);
            int result = 0;
            int i = 0;
            string[] results = new string[hash.Length/5];
            int z = 0;
            for (int p = 0; p < hash.Length/5; p++)
            {
                z += 5;
                while (i < z)
                {
                    results[p] += hash[i];
                    i++;
                }
            }

            List< int > IntFromResults = new List< int >(); -->
            List< int > dicesFromHash = new List< int >();

            foreach (var item in results)
            {
                int candidate = int.Parse(item, System.Globalization.NumberStyles.HexNumber);
                if (int.Parse(item, System.Globalization.NumberStyles.HexNumber) > 609999) continue;
                else
                {
                    candidate = can didate / 10000;
                    if (candidate > 60) throw new Exception("value must be lower then 60");
                    if (candidate < 1) continue;
                    if (candidate  < 1) throw new Exception("Error"); 
                    if ((candidate ) < 11 && candidate > 0) result = 1;
                    else if ((candidate) < 21) result = 2;
                    else if ((candidate) < 31) result = 3;
                    else if ((candidate) < 41) result = 4;
                    else if ((candidate) < 51) result = 5;
                    else if ((candidate) < 61) result = 6;
                    break;
                }                
            }
            if (i == 0) return GetDiceFromHash(serverSeed, clientSeed, nonce+= 1);
            return result; 
         }     
    }   
}
                        </pre></span><br><br><br></h6>

                </div>


            </div>
        </div>

   
        
</template>

<style lang="css">
</style>

<script>
import { mapActions } from "vuex";
import ProvablyFairApiService from "../services/ProvablyFairApiService";

export default {
  data() {
    return {
      serverSeedTest: "",
      clientSeedTest: "",
      nbOfDices: 0,
      clientSeeds: "",
      seeds: "",
      view: "provablyFair",
      dicesFromSeeds: [],
      errors: [],
      Responses: []
    };
  },

  mounted() {
    this.GetSeeds();
  },

  methods: {
    ...mapActions(["executeAsyncRequest"]),

    openjsfiddle() {
      window.open("https://jsfiddle.net/so1kv0uj/2/", "_blank");
    },

    async GetSeeds() {
      this.seeds = await this.executeAsyncRequest(() =>
        ProvablyFairApiService.GetSeeds()
      );
    },

    async RetriveDicesFromSeeds(e) {
      e.preventDefault();
      let toto = this;
      let azerty = await ProvablyFairApiService.RetriveDicesFromSeeds(
        this.serverSeedTest,
        this.clientSeedTest,
        this.nbOfDices
      ).then(function(azerty) {
        console.log("The last one");
        console.log(azerty);
        toto.dicesFromSeeds = azerty;
      });
    },

    async UpdateSeeds(e) {
      e.preventDefault();
      var errors = [];

      await this.executeAsyncRequest(() =>
        ProvablyFairApiService.UpdateSeeds(this.clientSeeds)
      );
      this.GetSeeds();
    },

    ChangeView(view) {
      this.view = view;
      this.errors = 0;
    }
  }
};
</script>

<style lang="scss">
// @import "compass/css3";

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
  from {
    bottom: 0;
    opacity: 0;
  }
  to {
    bottom: 30px;
    opacity: 1;
  }
}

@keyframes fadein {
  from {
    bottom: 0;
    opacity: 0;
  }
  to {
    bottom: 30px;
    opacity: 1;
  }
}

@-webkit-keyframes fadeout {
  from {
    bottom: 30px;
    opacity: 1;
  }
  to {
    bottom: 0;
    opacity: 0;
  }
}

@keyframes fadeout {
  from {
    bottom: 30px;
    opacity: 1;
  }
  to {
    bottom: 0;
    opacity: 0;
  }
}

.provablyfair html {
  overflow-y: scroll;
}

.provablyfair body {
  background: $body-bg;
  font-family: "Titillium Web", sans-serif;
}

.provablyfair .mytitle {
  text-decoration: none;
  color: $main;
  transition: 0.5s ease;
  &:hover {
    color: $main-dark;
  }
}

.provablyfair .form {
  background: rgba($form-bg, 0.9);
  padding: 40px;
  max-width: 6000px;
  margin: 40px auto;
  border-radius: $br;
  box-shadow: 0 4px 10px 4px rgba($form-bg, 0.3);
}

.provablyfair .tab-group {
  list-style: none;
  padding: 0;
  margin: 0 0 40px 0;
  &:after {
    content: "";
    display: table;
    clear: both;
  }
  li a {
    display: block;
    text-decoration: none;
    padding: 15px;
    background: rgba($gray-light, 0.25);
    color: $gray-light;
    font-size: 20px;
    float: left;
    width: 50%;
    text-align: center;
    cursor: pointer;
    transition: 0.5s ease;
    &:hover {
      background: $main-dark;
      color: $white;
    }
  }
  .active a {
    background: $main;
    color: $white;
  }
}

.provablyfair .tab-content > div:last-child {
  display: none;
}

.provablyfair h5 {
  text-align: center;
  color: $white;
  font-weight: $thin;
}

.provablyfair label {
  position: absolute;
  transform: translateY(6px);
  left: 13px;
  color: $white;
  transition: all 0.25s ease;
  -webkit-backface-visibility: hidden;
  pointer-events: none;
  font-size: 22px;
  .req {
    margin: 2px;
    color: #1ab188;
  }
}

.provablyfair input,
textarea {
  font-size: 22px;
  display: block;
  width: 100%;
  height: 100%;
  padding: 5px 10px;
  background: none;
  background-image: none;
  border: 1px solid $gray-light;
  color: $white;
  border-radius: 0;
  transition: border-color 0.25s ease, box-shadow 0.25s ease;
  &:focus {
    outline: 0;
    border-color: $main;
  }
}

.provablyfair textarea {
  border: 2px solid $gray-light;
  resize: vertical;
}

.provablyfair .field-wrap {
  position: relative;
  margin-bottom: 10px;
}

.provablyfair .button {
  border: 0;
  outline: none;
  border-radius: 0;
  padding: 15px 0;
  font-size: 2rem;
  font-weight: $bold;
  text-transform: uppercase;
  letter-spacing: 0.1em;
  background: $main;
  color: $white;
  transition: all.5s ease;
  -webkit-appearance: none;
  &:hover,
  &:focus {
    background: $main-dark;
  }
}

.provablyfair .button-block {
  display: block;
  width: 100%;
}

.provablyfair .forgot {
  margin-top: -20px;
  text-align: right;
}
</style>
 
