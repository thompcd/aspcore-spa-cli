<script>
    import { onMount } from 'svelte';
    import SortableList from 'svelte-sortable-list';
    import Test from '../components/Test.svelte';
    import { availableTests } from '../store/tests.js'
    import Select from 'svelte-select';

    
    let params;
    let exportName = "TestList";
    let partNumberInput = "";
    let selectedTests = [];
    let selectedStations;
    let selectedCells;
    $: isCreateDisabled = ( (selectedTests.length < 1) || 
    (partNumberInput.length <= 7) || 
    (selectedCells === undefined) || 
    (selectedStations === undefined) )

    const optionIdentifier = 'label';
    const placholderString = 'Search or Browse...'
    const sortSelectedList = ev => { selectedTests = (ev.detail) };
    const handleExport = () => { downloadObjectAsJson(selectedTests, exportName) };
    const groupBy = (item) => item.group;
    const displayString = (item) => `${item.value} - ${item.label}`;

    const stationItems = [
    {value: 'PREP', label: 'Prepare for Production', group: 'Testing'},
    {value: 'EOLT', label: 'End of Line Tester', group: 'Testing'},
    {value: 'PROG', label: 'End of Line Programmer', group: 'Programming'},
    {value: 'LEAK', label: 'Leak Tester', group: 'Assembly'},
    {value: 'VIZN', label: 'Visual Inspection', group: 'Assembly'},
    {value: 'GLUE', label: 'Automated Glue Machine', group: 'Assembly'},
    {value: 'BONE', label: 'PCBA Programmer', group: 'Programming'},
    {value: 'TINY', label: 'Tiny Tester', group: 'Testing'}
    ];
    
    const cellItems = [
    {value: '5812', label: 'HV1200 (SKINNY PEAT)', group: 'M2'},
    {value: '5812', label: 'PV1200 (SKINNY PEAT)', group: 'M2'},
    {value: '5812', label: 'HV1100 (BLACK LABEL)', group: 'M2'},
    {value: '5812', label: 'PV1100 (BLACK LABEL)', group: 'M2'},
    {value: '5813', label: 'PV350', group: 'IMX35'},
    {value: '5813', label: 'PV380', group: 'IMX35'},
    {value: '5813', label: 'MPC10 (V1)', group: 'TI'},
    {value: '5813', label: 'MPC10 (V2)', group: 'ST'},
    {value: '5813', label: 'MPC20 (V1)', group: 'TI'},
    {value: '5813', label: 'MPC20 (V2)', group: 'ST'},
    {value: '5813', label: 'CXM110', group: 'ST'},
    {value: '5814', label: 'HV450', group: 'IMX35'},
    {value: '5814', label: 'PV480', group: 'IMX35'},
    {value: '5814', label: 'PV485', group: 'IMX35'},
    {value: '5814', label: 'HV700', group: 'IMX35'},
    {value: '5814', label: 'PV780', group: 'IMX35'},
    {value: '5814', label: 'HV700BP', group: 'M2'},
    {value: '5814', label: 'PV780B', group: 'M2'},
    {value: '5821', label: 'FIREBALL', group: 'M2'},
    {value: '5823', label: 'FURIOUS', group: 'M2'},
    {value: '5824', label: 'HV750 (SCALLYWAG)', group: 'M2'},
    {value: '5827', label: 'TV480 (ORION)', group: 'IMX35'},
    {value: '5828', label: 'PV500 (PATRON)', group: 'G2'},
    {value: '5830', label: 'GPS PUCK', group: 'H8'},
    {value: '5831', label: 'LM4', group: 'G2'},
    {value: '5832', label: 'LM7', group: 'G2'},
    {value: '5875', label: 'BUTTONPAD', group: 'H8'},
    {value: '5875', label: 'PDM', group: 'H8'},
    {value: '5875', label: 'XMD', group: 'H8'},
    {value: '5809', label: 'PV101', group: 'MONOCOLOR'},
    {value: '5810', label: 'AQUASTAR', group: 'H8'},
	];

    function addTest(item) {
        const retVal = selectedTests.find( obj => obj === item)

        if (retVal){
            alert(`${item.testName} is already selected`)
            return
        }

        selectedTests.push(item)
        selectedTests = selectedTests;
    }

    function removeTest(item) {
        const objIndex = selectedTests.findIndex( obj => obj === item)

        if (objIndex > -1) {
            selectedTests.splice(objIndex, 1);
            selectedTests = selectedTests;
        }
    }

    function downloadObjectAsJson(exportObj, exportName){
        var dataStr = "data:text/json;charset=utf-8," + encodeURIComponent(JSON.stringify(exportObj, null, "\t"));
        var downloadAnchorNode = document.createElement('a');
        downloadAnchorNode.setAttribute("href",     dataStr);
        downloadAnchorNode.setAttribute("download", exportName + ".json");
        document.body.appendChild(downloadAnchorNode); // required for firefox
        downloadAnchorNode.click();
        downloadAnchorNode.remove();
    }

</script>

    <div class="lists">
        <form>
            <h1>Test Builder</h1>
            <div>
                <label>Part Number</label>
                <input type="text" bind:value={partNumberInput} placeholder="e.g. 78350777"/>
            </div>
            <div class="select">
                <label>Station Type</label>
                <Select items={stationItems} bind:selectedValue={selectedStations} placeholder={placholderString} {groupBy}></Select>
            </div>
            <div class="select">
                <label>Production Cell</label>
                <Select items={cellItems} bind:selectedValue={selectedCells} placeholder={placholderString} {groupBy} {optionIdentifier}></Select>
            </div>
            <!-- <div>
            <label> File Name</label>
            <input type="text" bind:value={exportName} placeholder="file name"/>
            </div> -->
            <div>
                <button type="button" class="btn" class:btn-primary="{isCreateDisabled}" 
                class:btn-success="{!isCreateDisabled}" on:click={handleExport} 
                disabled={isCreateDisabled}>Save Test List
                </button>
                <small id="emailHelp" class="form-text text-muted">After selecting tests, you can download the list.</small>
            </div>
        </form>

        <div class="tests" ref="tests">
            <h2>Available Tests</h2>
            <small class="form-text text-muted section">These are the default tests supported at the moment.<br>Feel free to edit some limits and add them to your current test list.</small>

            {#if $availableTests}
                {#each $availableTests as item, index}
                <Test {item} {index} collapsible={true}>
                    <button slot="actionButton" type="button" class="btn btn-warning" on:click={()=> addTest(item)}>
                    Add To Selected Tests
                    </button>
                </Test>
                {/each}
            {:else}
                <p><em>Loading...</em></p>
            {/if}
        </div>
        <div class="tests">
            <h2 class="text-warning">Selected Tests</h2>
            <small class="form-text text-muted section">These are the tests that get saved.<br>Drag them into the order you want to test them!</small>

            {#if selectedTests.length}
                <SortableList 
                bind:list={selectedTests} 
                    key="id" 
                on:sort={sortSelectedList}
                let:item
                let:index >
                    <Test {item} {index} collapsible={false}>
                        <span slot="indexNumber" class="orderIndex">{index+1}</span>
                        <button slot="actionButton" type="button" class="btn btn-danger" on:click={()=> removeTest(item)}>
                        Remove From Selected Tests
                        </button>
                    </Test>
                </SortableList>
            {:else}
                <div style="text-align:center; height: 300px; display: flex; align-items: center;">
                <em style="padding:4rem;">No tests selected</em>
                </div>
            {/if}
        </div>
    </div>

<style>
form{
    display:inline-block;
    margin-bottom: 4rem;
    min-width: 200px;
}
form div{
    display: flex;
    flex-direction: column;
    padding: 1rem 0;
}
form input{
    height: 42px;
    padding-left: 1rem;
    border-width: thin;
}

.lists{
    width: 100%;
    display: flex;
    flex-direction: row;
    justify-content: space-around;
}
.tests{
    width: 33rem;
    padding-left: 2rem;
    padding-right: 2rem;
}
.select{
    --borderRadius: 255px 25px 225px 25px/25px 225px 25px 255px;
    border-top-left-radius: 255px 25px;
    border-top-right-radius: 25px 225px;
    border-bottom-right-radius: 225px 25px;
    border-bottom-left-radius: 25px 255px;
    --placeholderColor: default;
    padding: 1rem 0;
    border: initial;
}

.section{
    /* fixed subtext height for alignment, check you don't have text collision if you adjust this */
    height: 2.2rem; 
    margin-bottom: .8rem;
    text-overflow: '';
    overflow: hidden;
}

small{
    padding-bottom: 2rem;
}

.btn-danger:hover{
    border: solid 1px rgba(220, 53, 69, 1);
    box-shadow: 0 5px 15px rgba(220, 53, 69, 1);
}

.orderIndex{
    display: inline-block;
    align-content: center;
    background-color: #ddd;
    border-radius: 4px;
    width: 4rem;
    text-align: center;
}

button:disabled,
button[disabled]{
  opacity: 0.65; 
  cursor: not-allowed;
}

.enabled{
    color: green;
}
</style>