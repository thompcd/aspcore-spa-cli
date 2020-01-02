<script>
    import {slide} from 'svelte/transition';
    import { availableTests } from '../store/tests.js'
    import { createEventDispatcher } from 'svelte';

	const dispatch = createEventDispatcher();

	export let item;
    export let collapsible = false;
    export let index;
    export let marginBottom = 0;
    export let canCommunications = true;

    let collpased = false;

    $: isLowerLimitValid = ValidateInput(item.testValueType, item.lowerLimit)
    $: isUpperLimitValid = ValidateInput(item.testValueType, item.upperLimit)
    $: isUpperLimitValid = ValidateInput(item.testValueType, item.upperLimit)
    $: isSendByte1Valid = ValidateInput("Byte", item.sendByte1)
    $: isSendByte2Valid = ValidateInput("Byte", item.sendByte2)
    $: isSendByte3Valid = ValidateInput("Byte", item.sendByte3)
    $: isSendByte4Valid = ValidateInput("Byte", item.sendByte4)
    $: isSendByte5Valid = ValidateInput("Byte", item.sendByte5)
    $: isSendByte6Valid = ValidateInput("Byte", item.sendByte6)
    $: isSendByte7Valid = ValidateInput("Byte", item.sendByte7)

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
            case "Byte":{
                return (!Number.isNaN(value));
            }
        
            default:{
                console.log("unhandled form validation type", item.testValueType)
                return false
            }
        }
    }

</script>

<div class="test-entry" class:selected="{collapsible === false}" style="margin-bottom:{marginBottom}rem">
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
                    placeholder="Byte"
                    class:is-valid="{isUpperLimitValid}"
                    class:is-invalid="{!isUpperLimitValid}"
                    class="form-control">
                </div>
                <div class="form-group">
                    <label class="form-control-label">Lower Limit</label>
                    <input type="string" step="any" bind:value={item.lowerLimit} 
                    placeholder="Byte"
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
            {#if canCommunications}
                <div class="can-container">
                SEND PGN COMMAND
                <div class="can-list-wrapper">
                <ul class="can-list">
                    <div class="can-item">
                        <label>Byte 1</label>
                        <input type="string" step="any" bind:value={item.sendByte1} 
                        placeholder="Byte"
                        class:is-valid="{isSendByte1Valid}"
                        class:is-invalid="{!isSendByte1Valid}"
                        class="form-control">
                    </div>
                    <div class="can-item">
                        <label>Byte 2</label>
                        <input type="string" step="any" bind:value={item.sendByte2} 
                        placeholder="Byte"
                        class:is-valid="{isSendByte2Valid}"
                        class:is-invalid="{!isSendByte2Valid}"
                        class="form-control">
                    </div>
                    <div class="can-item">
                        <label>Byte 3</label>
                        <input type="string" step="any" bind:value={item.sendByte3} 
                        placeholder="Byte"
                        class:is-valid="{isSendByte3Valid}"
                        class:is-invalid="{!isSendByte3Valid}"
                        class="form-control">
                    </div>
                    <div class="can-item">
                        <label>Byte 4</label>
                        <input type="string" step="any" bind:value={item.sendByte4} 
                        placeholder="Byte"
                        class:is-valid="{isSendByte4Valid}"
                        class:is-invalid="{!isSendByte4Valid}"
                        class="form-control">
                    </div>
                    <div class="can-item">
                        <label>Byte 5</label>
                        <input type="string" step="any" bind:value={item.sendByte5} 
                        placeholder="Byte"
                        class:is-valid="{isSendByte5Valid}"
                        class:is-invalid="{!isSendByte5Valid}"
                        class="form-control">
                    </div>
                    <div class="can-item">
                        <label>Byte 6</label>
                        <input type="string" step="any" bind:value={item.sendByte6} 
                        placeholder="Byte"
                        class:is-valid="{isSendByte6Valid}"
                        class:is-invalid="{!isSendByte6Valid}"
                        class="form-control">
                    </div>
                    <div class="can-item">
                        <label>Byte 7</label>
                        <input type="string" step="any" bind:value={item.sendByte7} 
                        placeholder="Byte"
                        class:is-valid="{isSendByte7Valid}"
                        class:is-invalid="{!isSendByte7Valid}"
                        class="form-control">
                    </div>
                </ul>
                </div>
                
                </div>
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
        /* margin-bottom: 1rem; */
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
    .can-container{
        width: 100%;
        border-left: solid 1px black;
        border-right: solid 1px black;
    }
    .can-list{
        display: flex;
        flex-direction: row;
    }
    .can-item{
        display: flex;
        flex-direction: column;
        margin: 0.2rem;
        width: 4.4rem;
        align-items: center;
    }
    .can-list-wrapper{
        border-top: solid 1px black;
        /* border-left: solid 1px black;
        border-right: solid 1px black; */
    }
    li{
        list-style: none;
    }

</style>