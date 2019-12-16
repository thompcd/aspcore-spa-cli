<script>
    import { selectedTests } from '../store/stores.js';
    import AddTest from './AddTest.svelte';
    import {slide} from 'svelte/transition';

	export let item;
    export let index;

	let addDescrVisible = false;
    
    console.log("list item", item)
</script>

<style>
	span{
		background-color: #ddd;
		border-radius: 4px;
		padding: 0 1em;
		font-size: .5em;
	}
    .test-entry{
        padding: 1rem;
        border: solid 1px #AAA;
        border-radius: 1rem;
    }

</style>

<div class="test-entry">
    <h1>
        <span>{index+1}</span> {item.testName}
    </h1>
    <button type="button" class="btn btn-outline-secondary" on:click={() => addDescrVisible = !addDescrVisible}>
	Expand / Collapse
    </button>
    <AddTest />
    {#if addDescrVisible}
    <div transition:slide>
        <fieldset class="form-group">
            <legend>Sliders</legend>
            <label for="upperLimit">Upper Limit</label>
            <input type="range" class="custom-range" id="upperLimit" data-com.bitwarden.browser.user-edited="yes">
        </fieldset>
        <p>UpperLimit: {item.upperLimit}</p>
        <p>LowerLimit: {item.lowerLimit}</p>
    </div>
    {/if}

</div>