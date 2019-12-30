<script>
    import {slide} from 'svelte/transition';
    import { availableTests } from '../store/tests.js'
    import { createEventDispatcher } from 'svelte';

	const dispatch = createEventDispatcher();

	export let item;
    export let collapsible = false;
    export let index;

    let collpased = false;

    $: isLowerLimitValid = ValidateInput(item.testValueType, item.lowerLimit)
    $: isUpperLimitValid = ValidateInput(item.testValueType, item.upperLimit)

    function ValidateInput(valueType, value){
        switch (valueType) {
            case "PassFail":{
                return (value === "Pass" || value === "Fail");
            }
            case "Double":{
                return (Number.parseFloat(value));
            }
            case "String":{
                return (value !== undefined);
            }
            case "Bool":{
                return (value === true || value === false);
            }
            case "Int":{
                return (Number.isInteger(value));
            }
        
            default:{
                console.log("unhandled form validation type", item.testValueType)
                return false
            }
        }
    }

</script>

<div class="test-entry" class:selected="{collapsible === false}">
    <h1>
        <slot name="indexNumber"><!-- optional fallback --></slot>
        {item.testName}
    </h1>
    {#if collapsible}
    <button type="button" class="btn btn-outline-secondary" on:click={() => collpased = !collpased}>
	View Details
    </button>
    {/if}

    <slot name="actionButton"><!-- optional fallback --></slot>

    {#if collpased}
    <div transition:slide class="details">
        <form id="availableTest{item.id}">
            {#if item.testValueType === "PassFail"}
                <p>Test Value Type: Pass / Fail</p>
            {:else if  item.testValueType === "Double"}
                <div class="form-group">
                    <label class="form-control-label">Upper Limit</label>
                    <input type="string" step="any" bind:value={item.upperLimit} 
                    placeholder={item.testValueType} 
                    class:is-valid="{isUpperLimitValid}"
                    class:is-invalid="{!isUpperLimitValid}"
                    class="form-control">
                </div>
                <div class="form-group">
                    <label class="form-control-label">Lower Limit</label>
                    <input type="string" step="any" bind:value={item.lowerLimit} 
                    placeholder={item.testValueType} 
                    class:is-valid="{isLowerLimitValid}"
                    class:is-invalid="{!isLowerLimitValid}"
                    class="form-control">
                </div>
            {:else if  item.testValueType === "String"}
                <!-- else if content here -->
            {:else if  item.testValueType === "Bool"}
                <!-- else if content here -->
            {:else if  item.testValueType === "Int"}
                <!-- else if content here -->
            {:else}
                <!-- else content here -->
            {/if}
        </form>
    </div>
    {/if}

</div>

<style>
    h1{
        margin-bottom: 1rem;
    }
    .test-entry{
        padding: 1rem;
        border: solid 1px #AAA;
        border-radius: 1rem;
        margin-bottom: 1rem;
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