new Cleave('.roNumber', {
    prefix: 'T',
    delimiter: ' ',
    blocks: [1, 5],
    uppercase: true
});

new Cleave('.opentimeFormat', {
    time: true,
    timePattern: ['h', 'm'],
    timeFormat: '24'
});

new Cleave('.closetimeFormat', {
    time: true,
    timePattern: ['h', 'm'],
    timeFormat: '24'
});