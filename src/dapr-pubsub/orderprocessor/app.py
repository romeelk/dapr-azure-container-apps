import random
from time import sleep    
import logging
import json
import datetime
from dapr.clients import DaprClient

#code
logging.basicConfig(level = logging.INFO)
orderStatuses = ["Pending","Complete","Processing"]

while True:
    sleep(random.randrange(50, 5000) / 1000)
    orderId = random.randint(1, 1000)
    orderStatusIndex = random.randint(0,len(orderStatuses)-1)
    orderStatus = orderStatuses[orderStatusIndex]

    PUBSUB_NAME = 'order_pub_sub'
    TOPIC_NAME = 'orders'
    order = {
        "id":orderId,
        "status":orderStatus,
        "orderDate":datetime.datetime.now().isoformat()
    }
    with DaprClient() as client:
        #Using Dapr SDK to publish a topic
        result = client.publish_event(
            pubsub_name=PUBSUB_NAME,
            topic_name=TOPIC_NAME,
            data=json.dumps(order),
            data_content_type='application/json',
        )
    logging.info('Published order: ' + str(order))