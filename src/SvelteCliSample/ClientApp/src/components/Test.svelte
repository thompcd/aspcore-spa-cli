<script>
    import {slide} from 'svelte/transition';
    import { availableTests } from '../store/tests.js'
    import { createEventDispatcher } from 'svelte';

	const dispatch = createEventDispatcher();

	export let item;
    export let collapsible = false;
    let index;
    let addDescrVisible = false;

</script>

<style>
    h1{
        margin-bottom: 1rem;
    }
    .test-entry{
        padding: 1rem;
        border: solid 1px #AAA;
        border-radius: 1rem;
    }
    .selected:hover{
        cursor: move;
        /* same color as "btn-warning", but rgba for opacity */
        border: solid 1px rgb(255, 193, 7);
        background: rgba(255, 193, 7, 0.05);
    }
    .details{
        padding: 1rem;
        padding-bottom: 0;
    }

</style>

<div class="test-entry" class:selected="{collapsible === false}">
    <h1>
        <slot name="indexNumber"><!-- optional fallback --></slot>
        {item.testName}
    </h1>
    {#if collapsible}
    <button type="button" class="btn btn-outline-secondary" on:click={() => addDescrVisible = !addDescrVisible}>
	View Details
    </button>
    {/if}

    <slot name="actionButton"><!-- optional fallback --></slot>

    {#if addDescrVisible}
    <div transition:slide class="details">
        <p>Test Value Type: {item.testValueType}</p>
        <p>Upper Limit: </p> <input type="text" bind:value={item.upperLimit} placeholder={item.testValueType}>
        <p>Lower Limit: {item.lowerLimit}</p>
    </div>
    {/if}

</div>