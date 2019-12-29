<script>
    import { onMount } from 'svelte';
    import SortableList from 'svelte-sortable-list';
    import Test from '../components/Test.svelte';
    import { availableTests } from '../store/tests.js'
    import Select from 'svelte-select';
    import Modal from '../components/Modal.svelte';
    
    let params;
    let exportName = "TestList";
    let partNumberInput = "";
    let selectedTests = [];
    let selectedStations;
    let selectedCells;
    let showDuplicateTestsModal = false;
    let showInstructions = false;

    $: isPartNumberValid =  partNumberInput.length > 7;

    $: isCreateDisabled = ( (selectedTests.length < 1) || 
    (!isPartNumberValid) || 
    (selectedCells === undefined) || 
    (selectedStations === undefined) )
    
    $: {
        if (!isCreateDisabled){
            exportName = `${partNumberInput}-${selectedCells.value}-${selectedStations.value}`;
        }
    }

    const optionIdentifier = 'label';
    const placholderString = 'Search or Browse...'
    const sortSelectedList = ev => { selectedTests = (ev.detail) };
    const handleExport = () => { downloadObjectAsJson(selectedTests, exportName) };
    const handleClickHelp = () => { showInstructions = true };
    const groupBy = (item) => item.group;

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
            showDuplicateTestsModal = true;
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
    {#if showDuplicateTestsModal}
        <Modal on:close="{() => showDuplicateTestsModal = false}">
            <div slot="header">
                <h2 class="text-danger">
                Duplicate test selected<br>
                </h2>
            </div>

            <p>This test has already been selected, test entries must be unique.
            <br>
            If you would like to run a test multiple times, this feature is currently only available in engineering mode of the SPOT application.</p>
        </Modal>
    {/if}
    <div class="lists">
        {#if showInstructions}
        <Modal on:close="{() => showInstructions = false}">
            <div slot="header">
                <h2 class="text-info">
                How to use the test builder
                </h2>
            </div>
            <ol>
                <li>Enter the part number for the product of the test list you'd like to create</li>
                <li>Choose which tester you'd like to build a test list for</li>
                <li>Choose the family the part belongs to (will be removed for existing part numbers in the future)</li>
                <li>Click to expand any available test, view and edit its limits, then 'add to selected'</li>
                <li>Re-arrange your 'selected tests' to run in the order you'd prefer</li>
                <li>Click 'Save test list' to download your file</li>
            </ol>
            <hr>
            <h2 class="text-info">What is the test builder?</h2>
            <p>The test builder allows a user to easily view what software capabilities a tester currently supports and gives a simple interface to adjust limits for existing parts and create limits for new parts.</p>
        </Modal>
    {/if}
        <form>
            <div id="heading-helper">
                <h1>Test Builder</h1>
                <button type="button" class="info btn-info" on:click={ handleClickHelp }>?</button>
            </div>
            <div class="form-group has-success">
                <label>Part Number</label>
                <input type="text" class="form-control" class:is-valid="{isPartNumberValid}" bind:value={partNumberInput} placeholder="e.g. 78350777"/>
            </div>
            <div class="select">
                <label>Station Type</label>
                <Select items={stationItems} bind:selectedValue={selectedStations} placeholder={placholderString} {groupBy}></Select>
            </div>
            <div class="select">
                <label>Production Cell</label>
                <Select items={cellItems} bind:selectedValue={selectedCells} placeholder={placholderString} {groupBy} {optionIdentifier}></Select>
            </div>
            <div>
                <button type="button" class="btn" class:btn-primary="{isCreateDisabled}" 
                class:btn-success="{!isCreateDisabled}" on:click={handleExport} 
                disabled={isCreateDisabled}>Save Test List
                </button>
                <small class="form-text text-muted">After selecting tests, you can download the list.</small>
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
    border-color: rgba(63, 79, 95, 0.25);
    font-weight: 200;
}
form input:focus{
    border-color: var(--borderFocusColor, #006FE8);
    outline: none;
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

#heading-helper{
    display: inline-flex;
    flex-direction: row;
}
#heading-helper button{
    font-size: 1.6rem;
    margin-left: 1rem;
    margin-top: 0.4rem;
    height: 2rem;
    width: 2rem;
    border-radius: 2rem;
    text-align: center;
}
</style>