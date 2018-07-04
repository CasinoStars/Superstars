<template>
<div class="home">
    <div class="bs-example">
        <div id="myCarousel" class="carousel slide" data-ride="carousel">
            <!-- Carousel indicators -->
            <ol class="carousel-indicators">
                <li data-target="#myCarousel" data-slide-to="0" class="active"></li>
                <li data-target="#myCarousel" data-slide-to="1"></li>
                <li data-target="#myCarousel" data-slide-to="2"></li>
            </ol>   
            <!-- Wrapper for carousel items -->
            <div class="carousel-inner" style="text-align: center; font-family: 'Courier New', sans-serif;">
                <div class="carousel-item active">
                    <img src="../img/../img/provablylogo.png">
                    <div>Un site provably Fair</div>
                </div>
                <div class="carousel-item">
                    <img src="../img/fastbetlogo.png">
                    <div>Des jeux rapides</div>
                </div>
                <div class="carousel-item">
                    <img src="../img/statslogo.png">
                    <div>Des statistiques détaillées</div>
                </div>
            </div>
            <!-- Carousel controls -->
            <a class="carousel-control left" href="#myCarousel" data-slide="prev">
                <span class="glyphicon glyphicon-chevron-left"></span>
            </a>
            <a class="carousel-control right" href="#myCarousel" data-slide="next">
                <span class="glyphicon glyphicon-chevron-right"></span>
            </a>
        </div>
    </div>
</div>
</template>

<script>
import { mapActions } from 'vuex';
import Vue from 'vue';

export default {

     methods: {
    ...mapActions(['executeAsyncRequest']),
     

     } 
}
</script>

<style lang="css">
.home .carousel{
    background: rgb(235,235,235);
    margin-top: 1%;
}
.home .carousel .carousel-item{
    min-height: 350px;/* Prevent carousel from being distorted if for some reason image doesn't load */
}

.home .carousel .carousel-item img{
    width: 150px;
    height: 160px;
    margin-top: 5%; /* Align slide image horizontally center */
}
</style>


  