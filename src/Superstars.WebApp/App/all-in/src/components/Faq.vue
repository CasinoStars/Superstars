<template>
<div class="page">
<div class="jumbotron jumbotron-fluid">
  <div class="container">
    <h1 class="text-info">All`in ?</h1>
    <p class="lead">All`in est un Bitcoin-Casino (comprenez un Casino où vous pouvez parier des crypto-monnaies tels que le Bitcoin) comprenant plusieurs jeux. 
      All`in dispose actuellement de 2 monnaies différentes : <br><br> - le <strong>Bitcoin</strong>, une crypto-monnaie pouvant être utilisée, achetée et vendue dans le monde entier
      <br><br> - le <strong>All`in Coin</strong>, une monnaie n'ayant aucune valeure financière, et pouvant être utilisée uniquement sur ce site
    </p>
  </div>
</div>
<div class="jumbotron jumbotron-fluid">
  <div class="container">
    <h1 class="text-info">Comment puis-je jouer ?</h1>
    <p class="lead">Vous avez juste besoin de créer gratuitement un compte ! Vous pourrez ensuite déposer vos Bitcoin, ou bien de la monnaie All`in, via l'interface <strong> Portefeuille. </strong> </p>
  </div>
</div>
<div class="jumbotron jumbotron-fluid">
  <div class="container">
    <h1 class="text-info">Est-ce que le Casino est intègre ?</h1>
    <p class="lead">Oui ! Et nous pouvons le prouver ! Obtenez plus de détails dans l'onglet <router-link to=/Account> <strong>'Provably-Fair'</strong></router-link> de votre profil.
     <br> Pour plus de transparence, voici des outils vous permettant de le vérifier : </p>
    
    <p style="text-align:center;" class="lead">
      <a href="https://dotnetfiddle.net/49eMJn?fbclid=IwAR0AJ2tqLOpWUwROhIsLFWXSfXn5A-bUDcahBwGhB8MxFTYMxLgRQpsXAuM" onclick="window.open(this.href); return false;">Provably
        Fair Yams</a> / 
      <a href="https://dotnetfiddle.net/KObOc8?fbclid=IwAR0RMkGzFzNBaoE7CsbC623vPOXXQrOUpKultgdw6NUUluUHEpvOqj9l57Q" onclick="window.open(this.href); return false;">Provably
        Fair BlackJack</a> / 
      <a href="https://jsfiddle.net/hloiseau/no3ecyu7/embedded/result" onclick="window.open(this.href); return false;">Provably Fair Crash</a>
    </p>
  </div>
</div>
<div class="jumbotron jumbotron-fluid">
  <div class="container">
    <h1 class="text-info">Quelle est la marge du Casino ? </h1>
    <p class="lead">La marge du Casino est actuellement d'environ <strong>1%</strong> ! Nous ne prenons aucun frais sur les transactions, notre seule marge est effectuée sur l'expertise des ordinateurs contre lesquels vous jouez.</p>
  </div>
</div>
</div>
</template>

<script>
    export default {
        mounted() {
        
        },

        beforeDestroy() {

        },

        methods: {

        }
    }
</script>

<style lang="scss">
.page {
    padding-top: 1.5%;
}
</style>